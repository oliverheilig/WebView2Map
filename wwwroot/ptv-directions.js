/*
 * maplibre-gl-directions calculates its routes with OSRM. This subclass calculates them with the
 * PTV Developer Routing API instead: https://developer.myptv.com/en/documentation/routing-api
 */
import MapLibreGlDirections, {
    MapLibreGlDirectionsCancelableEvent,
    MapLibreGlDirectionsNonCancelableEvent,
} from "https://unpkg.com/@maplibre/maplibre-gl-directions@0.9.1/dist/maplibre-gl-directions.js";

export default class PtvDirections extends MapLibreGlDirections {
    constructor(map, apiKey, configuration) {
        super(map, configuration);
        this.apiKey = apiKey;
    }

    /*
     * Modified copy of
     * https://github.com/maplibre/maplibre-gl-directions/blob/v0.9.1/src/directions/main.ts#L130
     */
    async fetchDirections(originalEvent) {
        /*
         * If a request from a previous fetchDirections is already running (there's no such a check really, but this is
         * implied be calling this method), we abort it as we don't need the previous value anymore.
         */
        this.abortController?.abort();

        if (this._waypoints.length >= 2) {
            if (!this.fire(new MapLibreGlDirectionsCancelableEvent("fetchroutesstart", originalEvent, {}))) return;

            this.abortController = new AbortController();

            let timer;
            if (this.configuration.requestTimeout !== null) {
                timer = setTimeout(() => this.abortController?.abort(), this.configuration.requestTimeout);
            }

            // The waypoints are GeoJSON points ([lng, lat]), PTV expects lat,lng
            let waypointsQuery = "";
            for (const wp of this._waypoints) {
                waypointsQuery += `&waypoints=${wp.geometry.coordinates[1]},${wp.geometry.coordinates[0]}`;
            }

            const url = `https://api.myptv.com/routing/v1/routes?results=LEGS_POLYLINE&options[trafficMode]=AVERAGE${waypointsQuery}&apiKey=${this.apiKey}`;

            // Stays undefined if the request fails, like in the original
            let response;
            try {
                try {
                    const httpResponse = await fetch(url, { signal: this.abortController?.signal });
                    const json = await httpResponse.json();
                    if (!httpResponse.ok)
                        throw new Error(json.description ?? `Routing request failed with HTTP ${httpResponse.status}`);
                    response = json;
                } finally {
                    this.fire(
                        new MapLibreGlDirectionsNonCancelableEvent("fetchroutesend", originalEvent, { directions: response }),
                    );
                }
            } finally {
                clearTimeout(timer);
            }

            // decode the JSON polyline
            for (const leg of response.legs)
                leg.polyline = JSON.parse(leg.polyline);

            let snappoints = [];
            for (let i = 0; i < this._waypoints.length; i++)
                snappoints.push({
                    type: "Feature",
                    geometry: {
                        type: "Point",
                        coordinates:
                            // use the first coord of the corresponding leg
                            // and the last coord of the last leg (n waypoints, n-1 legs)
                            i < this._waypoints.length - 1
                                ? response.legs[i].polyline.coordinates[0]
                                : response.legs[i - 1].polyline.coordinates[
                                response.legs[i - 1].polyline.coordinates.length - 1
                                ],
                    },
                    properties: {
                        type: "SNAPPOINT",
                        id: "sp" + i,
                        waypointProperties: this._waypoints[i].properties ?? {},
                    },
                });

            let routelines = [];
            for (let i = 0; i < response.legs.length; i++)
                routelines.push([
                    {
                        type: "Feature",
                        geometry: response.legs[i].polyline,
                        properties: {
                            id: "rl" + i,
                            routeIndex: 0,
                            route: "SELECTED",
                            legIndex: i,
                            congestion: 0,
                            departSnappointProperties: snappoints[i].properties,
                            arriveSnappointProperties: snappoints[i + 1].properties,
                        },
                    },
                ]);
            this.snappoints = snappoints;
            this.routelines = routelines;
        } else {
            this.snappoints = [];
            this.routelines = [];
        }

        // the selected route index might have changed
        this.draw(false);
    }
}
