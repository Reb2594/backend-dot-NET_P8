using GpsUtil.Location;

namespace TourGuide.Models
{
    public class NearbyAttractionDto
    {
        public string AttractionName { get; set; }

        public Locations AttractionLocation { get; set; }
        public Locations UserLocation { get; set; }

        public double DistanceInMiles { get; set; }
        public int RewardPoints { get; set; }

        public NearbyAttractionDto(
        string attractionName,
        Locations attractionLocation,
        Locations userLocation,
        double distanceInMiles,
        int rewardPoints)
        {
            AttractionName = attractionName;
            AttractionLocation = attractionLocation;
            UserLocation = userLocation;
            DistanceInMiles = distanceInMiles;
            RewardPoints = rewardPoints;
        }
    }
}
