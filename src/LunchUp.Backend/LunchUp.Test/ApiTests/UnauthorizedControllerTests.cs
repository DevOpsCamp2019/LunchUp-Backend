using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace LunchUp.Test.ApiTests
{
    public class UnauthorizedControllerTests : IClassFixture<LunchUpFactory<UnauthorizedStartup>>
    {
        private readonly LunchUpFactory<UnauthorizedStartup> _factory;

        public UnauthorizedControllerTests(LunchUpFactory<UnauthorizedStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_WhenGetMatches_ThenUnauthorizedReturned()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/match");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
