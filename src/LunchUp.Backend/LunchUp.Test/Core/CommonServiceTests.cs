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
            var person = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com")
                .WithFirstname("John").WithLastname("Doe").BuildSaved();

            // Act
            var result = _commonService.GetPersonExistStatus(person.Email);
            
            // Assert
            Assert.Equal(person.Firstname, result.Firstname);
            Assert.Equal(person.Lastname, result.Lastname);
            Assert.Equal(person.Email, result.Email);
            Assert.NotNull(result.OptIn);
        }

        [Fact]
        public void GivenEmptyDatabase_WhenGetPersonExistStatus_ThenNullReturned()
        {
            // Act
            var result = _commonService.GetPersonExistStatus("john.doe@anonymous.com");

            // Assert
            Assert.Null(result);
        }
    }
}
