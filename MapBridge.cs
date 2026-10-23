using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace WebView2Map
{
    /// <summary>
    /// The host object that wwwroot/routing.html calls as chrome.webview.hostObjects.sync.bridge.
    /// Only the public members of this class are reachable from JavaScript.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class MapBridge
    {
        private readonly MainForm form;

        public MapBridge(MainForm form)
        {
            this.form = form;
        }

        // A route has been calculated: distance in meters, travel time in seconds
        public void onCalculated(int distance, int travelTime) => form.ShowRoute(distance, travelTime);

        // The waypoints have changed on the map: JSON array of [lng, lat]
        public void waypointsChanged(string waypointsJson) =>
            form.ShowWaypoints(JsonConvert.DeserializeObject<double[][]>(waypointsJson));
    }
}
