using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebView2Map
{
    public partial class MainForm : Form
    {
        // Get your free key at https://developer.myptv.com/
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("PTV_API_KEY") ?? "";

        // wwwroot is served under this host name, because a file:// page can't import local JavaScript modules
        private const string AppHost = "appassets.example";

        // The waypoints shown in the grid
        private readonly BindingList<Waypoint> waypoints = new BindingList<Waypoint>();

        public MainForm()
        {
            InitializeComponent();
            waypointBindingSource.DataSource = waypoints;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (ApiKey.Length == 0)
                MessageBox.Show("Set the environment variable PTV_API_KEY to your PTV Developer API key.\n" +
                    "You can get a free key at https://developer.myptv.com/", Text);

            // Initialize WebView2, expose the bridge to JavaScript, then load the page
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.AddHostObjectToScript("bridge", new MapBridge(this));
            webView.CoreWebView2.SetVirtualHostNameToFolderMapping(AppHost,
                Path.Combine(Application.StartupPath, "wwwroot"), CoreWebView2HostResourceAccessKind.DenyCors);
            webView.Source = new Uri($"https://{AppHost}/routing.html");
        }

        // Runs after every page load, including a reload with F5
        private async void webView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
                return;

            await CallScriptAsync("initMapAndRouting", ApiKey, 8.4, 49.015, 12); // lng, lat, zoom
            await SetWaypointsAsync(new[]
            {
                new Waypoint(49.01502, 8.37922),
                new Waypoint(49.01328, 8.42806),
            });
        }

        private async void getRouteButton_Click(object sender, EventArgs e)
        {
            await SetWaypointsAsync(waypoints);
        }

        // Host -> JavaScript

        private Task SetWaypointsAsync(IEnumerable<Waypoint> list)
        {
            // MapLibre and GeoJSON use [lng, lat]
            return CallScriptAsync("setWaypoints", list.Select(wp => new[] { wp.Longitude, wp.Latitude }));
        }

        // Calls a global JavaScript function. Passing the arguments as JSON
        // takes care of string escaping and culture-invariant numbers.
        private Task CallScriptAsync(string function, params object[] args)
        {
            var jsonArgs = args.Select(arg => JsonConvert.SerializeObject(arg));
            return webView.ExecuteScriptAsync($"{function}({string.Join(",", jsonArgs)});");
        }

        // JavaScript -> Host, called by MapBridge

        internal void ShowRoute(int distance, int travelTime)
        {
            travelTimeLabel.Text = $"Travel Time: {TimeSpan.FromSeconds(travelTime)}";
            distanceLabel.Text = $"Distance: {distance / 1000.0} km";
        }

        internal void ShowWaypoints(double[][] coordinates)
        {
            waypoints.Clear();
            foreach (var c in coordinates)
                waypoints.Add(new Waypoint(latitude: c[1], longitude: c[0])); // c is [lng, lat]

            // The map is ready once it reports waypoints
            getRouteButton.Enabled = true;
        }
    }
}
