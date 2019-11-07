using System;
using LunchUp.Core.Matching;
using Xunit;

namespace LunchUp.Test.Core
{
    public class MatchingServiceTests : ServiceTestBase
    {
        private readonly IMatchingService _matchingService;

        public MatchingServiceTests()
        {
            _matchingService = new MatchingService(LunchUpContext);
        }

        [Fact]
        public void GivenExistingPersonEntityWithoutOptIn_WhenGetSuggestions_ThenNothingReturned()
        {
            // Arrange
            var p = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com").BuildSaved();

            // Act
            var result = _matchingService.GetSuggestions(new PersonEntity {Email = "bla"}, 1);

            // Assert            
            Assert.Empty(result);
        }

        [Fact]
        public void GivenExistingPersonEntityWithOptIn_WhenGetSuggestions_ThenPersonEntityReturned()
        {
            // Arrange
            var p = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com")
                .WithOptIn(DateTime.Now).BuildSaved();

            // Act
            var result = _matchingService.GetSuggestions(new PersonEntity {Email = "bla"}, 1);

            // Assert            
            Assert.Single(result);
        }

        [Fact]
        public void GivenOwnPersonEntityWithOptIn_WhenGetSuggestions_ThenNothingReturned()
        {
            // Arrange
            var p = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com")
                .WithOptIn(DateTime.Now).BuildSaved();

            // Act
            var result = _matchingService.GetSuggestions(new PersonEntity {Email = "john.doe@anonymous.com"}, 1);

            // Assert            
            Assert.Empty(result);
        }

        [Fact]
        public void GivenExistingPersonEntityWithResponse_WhenGetSuggestions_ThenNothingReturned()
        {
            // Arrange
            var origin = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com").BuildSaved();
            var target = new PersonEntityBuilder(LunchUpContext, "peter.meier@google.com")
                .WithOptIn(DateTime.Now).BuildSaved();
            var response = new ResponseBuilder(LunchUpContext, origin, target, true, DateTime.Now).BuildSaved();

            // Act
            var result = _matchingService.GetSuggestions(new PersonEntity {Email = "john.doe@anonymous.com"}, 1);

            // Assert            
            Assert.Empty(result);
        }
    }
}
