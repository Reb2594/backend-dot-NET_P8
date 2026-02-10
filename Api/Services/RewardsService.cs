using GpsUtil.Location;
using TourGuide.LibrairiesWrappers.Interfaces;
using TourGuide.Services.Interfaces;
using TourGuide.Users;
using TourGuide.Utilities;

namespace TourGuide.Services;

public class RewardsService : IRewardsService
{
    private const double StatuteMilesPerNauticalMile = 1.15077945;
    private readonly int _defaultProximityBuffer = 10;
    private int _proximityBuffer;
    private readonly int _attractionProximityRange = 200;
    private readonly IGpsUtil _gpsUtil;
    private readonly IRewardCentral _rewardsCentral;
    private static int count = 0;
    private List<Attraction> _attractions;

    public RewardsService(IGpsUtil gpsUtil, IRewardCentral rewardCentral)
    {
        _gpsUtil = gpsUtil;
        _rewardsCentral =rewardCentral;
        _proximityBuffer = _defaultProximityBuffer;
    }

    public void SetProximityBuffer(int proximityBuffer)
    {
        _proximityBuffer = proximityBuffer;
    }

    public void SetDefaultProximityBuffer()
    {
        _proximityBuffer = _defaultProximityBuffer;
    }

    public async Task InitializeAttractionsAsync()
    {
        _attractions = await _gpsUtil.GetAttractions();
    }

    public async Task CalculateRewards(User user)
    {
        var userVisitedLocations = user.VisitedLocations.ToList();

        foreach (var visitedLocation in userVisitedLocations)
        {
            await CalculateRewards(user, visitedLocation);
        }
    }

    public async Task CalculateRewards(User user, VisitedLocation visitedLocation)
    {
        var rewardedAttractions = new HashSet<string>(
            user.UserRewards.Select(r => r.Attraction.AttractionName)
        );

        var precomputedLocation = new PrecomputedLocation(visitedLocation.Location);

        foreach (var attraction in _attractions)
        {
            if (rewardedAttractions.Contains(attraction.AttractionName))
                continue;

            if (NearAttraction(precomputedLocation, attraction))
            {
                int rewardPoints = await GetRewardPointsAsync(attraction, user);
                user.AddUserReward(
                    new UserReward(visitedLocation, attraction, rewardPoints)
                );
            }
        }
    }

    public bool IsWithinAttractionProximity(Attraction attraction, Locations location)
    {
        var precomputedLocation = new PrecomputedLocation(location);
        return GetDistance(precomputedLocation, attraction) <= _attractionProximityRange;
    }

    private bool NearAttraction(PrecomputedLocation location, Attraction attraction)
    {
        return GetDistance(location, attraction) <= _proximityBuffer;
    }

    private async Task<int> GetRewardPointsAsync(Attraction attraction, User user)
    {
        return await _rewardsCentral.GetAttractionRewardPointsAsync(attraction.AttractionId, user.UserId);
    }

    public double GetDistance(PrecomputedLocation loc, Attraction attraction)
    {
        double angle = Math.Acos(
            loc.SinLat * attraction.SinLat
            + loc.CosLat * attraction.CosLat
            * Math.Cos(loc.LonRad - attraction.LonRad)
        );

        double nauticalMiles = 60.0 * angle * 180.0 / Math.PI;
        return StatuteMilesPerNauticalMile * nauticalMiles;
    }
}
