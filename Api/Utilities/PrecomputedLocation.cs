using GpsUtil.Location;

namespace TourGuide.Utilities
{
    public class PrecomputedLocation
    {
        public double LatRad { get; }
        public double LonRad { get; }
        public double SinLat { get; }
        public double CosLat { get; }

        public PrecomputedLocation(Locations location)
        {
            LatRad = Math.PI * location.Latitude / 180.0;
            LonRad = Math.PI * location.Longitude / 180.0;
            SinLat = Math.Sin(LatRad);
            CosLat = Math.Cos(LatRad);
        }
    }
}
