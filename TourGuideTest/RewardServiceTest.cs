using GpsUtil.Location;
using System;
using System.Linq;
using System.Threading.Tasks;
using TourGuide.Users;
using TourGuide.Utilities;
using Xunit;

namespace TourGuideTest
{
    public class RewardServiceTest : IClassFixture<DependencyFixture>
    {
        private readonly DependencyFixture _fixture;

        public RewardServiceTest(DependencyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UserGetRewards()
        {
            await _fixture.InitializeAsync(0);

            var user = new User(Guid.NewGuid(), "jon", "000", "jon@tourGuide.com");
            var attraction = (await _fixture.GpsUtil.GetAttractions()).First();

            user.AddToVisitedLocations(new VisitedLocation(user.UserId, attraction, DateTime.Now));

            await _fixture.TourGuideService.TrackUserLocationAsync(user);

            var userRewards = user.UserRewards;
            _fixture.TourGuideService.Tracker.StopTracking();

            Assert.True(userRewards.Count == 1);
        }

        [Fact]
        public async Task IsWithinAttractionProximity()
        {
            await _fixture.InitializeAsync(1);

            var attraction = (await _fixture.GpsUtil.GetAttractions()).First();
            Assert.True(_fixture.RewardsService.IsWithinAttractionProximity(attraction, attraction));
        }

        [Fact]
        public async Task NearAllAttractions()
        {
            await _fixture.InitializeAsync(1);

            _fixture.RewardsService.SetProximityBuffer(int.MaxValue);

            var user = _fixture.TourGuideService.GetAllUsers().First();

            await _fixture.RewardsService.CalculateRewards(user);

            var userRewards = _fixture.TourGuideService.GetUserRewards(user);
            _fixture.TourGuideService.Tracker.StopTracking();

            var attractionsCount = (await _fixture.GpsUtil.GetAttractions()).Count;
            Assert.Equal(attractionsCount, userRewards.Count);
        }
    }
}
