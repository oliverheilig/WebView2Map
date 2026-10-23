namespace WebView2Map
{
    public class Waypoint
    {
        // Needed so the grid can add new rows
        public Waypoint()
        {
        }

        public Waypoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
