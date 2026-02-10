using GpsUtil.Location;
using TourGuide.Users;
using TourGuide.Utilities;

namespace TourGuide.Services.Interfaces
{
    public interface IRewardsService
    {
        Task InitializeAttractionsAsync();
        Task CalculateRewards(User user);
        Task CalculateRewards(User user, VisitedLocation visitedLocation);
        double GetDistance(PrecomputedLocation loc, Attraction attraction);
        bool IsWithinAttractionProximity(Attraction attraction, Locations location);
        void SetDefaultProximityBuffer();
        void SetProximityBuffer(int proximityBuffer);
    }
}