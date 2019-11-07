using LunchUp.Core.Common;
using Xunit;

namespace LunchUp.Test.Core
{
    public class CommonServiceTests : ServiceTestBase
    {
        private readonly ICommonService _commonService;

        public CommonServiceTests()
        {
            _commonService = new CommonService(LunchUpContext);
        }

        [Fact]
        public void GivenPersonEntityInDatabase_WhenGetPersonExistStatus_ThenPersonEntityReturned()
        {
            // Arrange
            LunchUpContext.Person.Add(Entity);
            LunchUpContext.SaveChanges();

            // Act
            var result = _commonService.GetPersonExistStatus(Entity.Email);
            
            // Assert
            Assert.Equal(Entity.Firstname, result.Firstname);
            Assert.Equal(Entity.Lastname, result.Lastname);
            Assert.Equal(Entity.Email, result.Email);
            Assert.NotNull(result.OptIn);
        }

        [Fact]
        public void GivenEmptyDatabase_WhenGetPersonExistStatus_ThenNullReturned()
        {
            // Act
            var result = _commonService.GetPersonExistStatus(Entity.Email);

            // Assert
            Assert.Null(result);
        }
    }
}
