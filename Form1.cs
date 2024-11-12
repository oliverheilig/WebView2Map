using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WebView2Map
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public partial class Form1 : Form
    {
        private static readonly string apiKey = ""; // Get your free key at https://developer.myptv.com/;
        public Form1()
        {
            InitializeComponent();

            webViewControl.Source = new Uri($"file:///{Application.StartupPath}/routing.html");
        }

        WayPoint[] wayPoints = new [] {
            new WayPoint(8.37922, 49.01502),
            new WayPoint(8.42806, 49.01328),
        };

        public void onCalculated(int distance, int travelTime)
        {
            label1.Text = $"Travel Time: {TimeSpan.FromSeconds(travelTime)}" ;
            label2.Text = $"Distance: {distance / 1000.0} km";
        }

        private async void webViewControl_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // initialize the host bridge
            await webViewControl.EnsureCoreWebView2Async(null);
            webViewControl.CoreWebView2.AddHostObjectToScript("bridge", this);

            // initilaize maplibre
            double lat = 8.4;
            double lng = 49.015;
            int zoom = 12;
            await webViewControl.CoreWebView2.ExecuteScriptAsync(FormattableString.Invariant(
                $"window.initMapAndRouting('{apiKey}',{lat},{lng},{zoom});"));

            // Calculate initial route
            calculateRoute(wayPoints);
        }

        public void waypointsChanged(dynamic waypoints)
        {
            // Browser->Host seems to work with [][]double, alternative would be json
            var wpd = new List<WayPoint>();
            foreach (dynamic waypoint in waypoints)
                wpd.Add(new WayPoint(waypoint[0], waypoint[1]));

            var bindingList = new BindingList<WayPoint>(wpd) { AllowNew = true, AllowRemove = true };
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
        }

        private async void calculateRoute(IEnumerable<WayPoint> waypoints) 
        { 
            // For complex data Host->Browser just use json
            var coords = waypoints.Select(wp => new[] {wp.Latitude, wp.Longitude});
            string json = JsonConvert.SerializeObject(coords);
            await webViewControl.CoreWebView2.ExecuteScriptAsync($"setWaypoints({json});");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculateRoute((dataGridView1.DataSource as BindingSource).DataSource as IEnumerable<WayPoint>);
        }
    }

    public class WayPoint
    {
        public WayPoint()
        {
        }

        public WayPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
