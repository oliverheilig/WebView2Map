# WebView2Map
Sample for PTV Developer within a WinForms application

Demonstrates how to:
* Utilize [WebView2](https://developer.microsoft.com/microsoft-edge/webview2) to display the PTV Developer [Vector Maps API](https://developer.myptv.com/en/documentation/vector-maps-api).
* Integrate interactive routing using the PTV Developer [Routing Api](https://developer.myptv.com/en/documentation/routing-api) and [maplibre-gl-directions](https://github.com/maplibre/maplibre-gl-directions).
* Bridge api calls between Winforms and WebView2.

Get your free API key at https://developer.myptv.com/ and store it in the environment variable `PTV_API_KEY`, for example with `setx PTV_API_KEY "<your key>"`. Restart Visual Studio afterwards so it picks up the variable.

## How it fits together

* [MainForm.cs](MainForm.cs) hosts the WebView2 control. It serves the [wwwroot](wwwroot) folder as `https://appassets.example/` and registers [MapBridge.cs](MapBridge.cs) as the host object `bridge`.
* [wwwroot/routing.html](wwwroot/routing.html) creates the map and talks to the host.
* [wwwroot/ptv-directions.js](wwwroot/ptv-directions.js) makes maplibre-gl-directions calculate its routes with the PTV Developer Routing API.

The calls between the two sides:

| Direction | Call | Purpose |
|---|---|---|
| Host → JavaScript | `initMapAndRouting(apiKey, lng, lat, zoom)` | Creates the map |
| Host → JavaScript | `setWaypoints([[lng, lat], ...])` | Sets the waypoints and calculates the route |
| JavaScript → Host | `bridge.onCalculated(distance, travelTime)` | Route summary in meters and seconds |
| JavaScript → Host | `bridge.waypointsChanged(json)` | The waypoints as JSON `[[lng, lat], ...]` |

Coordinates are `[lng, lat]` on the JavaScript side, as everywhere in MapLibre and GeoJSON.

![screenshot](https://raw.githubusercontent.com/oliverheilig/WebView2Map/main/screenshot.png)
