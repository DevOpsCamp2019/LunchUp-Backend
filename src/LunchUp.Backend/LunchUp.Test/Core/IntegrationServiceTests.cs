using System.Collections.Generic;
using System.Linq;
using LunchUp.Core.Integration;
using LunchUp.Model.Models;
using Xunit;

namespace LunchUp.Test.Core
{
    public class IntegrationServiceTests : ServiceTestBase
    {
        private readonly IIntegrationService _integrationService;

        public IntegrationServiceTests()
        {
            _integrationService = new IntegrationService(LunchUpContext);
        }

        [Fact]
        public void GivenNewPersonEntity_WhenCreateOrUpdatePerson_ThenPersonEntityAdded()
        {
            // Arrange
            var p = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com").BuildUnsaved();

            // Act
            _integrationService.CreateOrUpdatePersons(new List<PersonEntity>() { p }).Wait();
            
            // Assert
            var result = LunchUpContext.Person.FirstOrDefault(p => p.Email == p.Email);
            Assert.NotNull(result);
            Assert.Equal(p, result);
        }

        [Fact]
        public void GivenExistingPersonEntity_WhenCreateOrUpdatePerson_ThenPersonEntityUpdated()
        {
            // Arrange
            var p = new PersonEntityBuilder(LunchUpContext, "john.doe@anonymous.com").BuildSaved();
            p.Firstname = "Peter";
            p.Lastname = "Meier";

            // Act
            _integrationService.CreateOrUpdatePersons(new List<PersonEntity>() { p }).Wait();

            // Assert
            var result = LunchUpContext.Person.FirstOrDefault(p => p.Email == p.Email);
            Assert.NotNull(result);
            Assert.Equal(p.Firstname, result.Firstname);
            Assert.Equal(p.Lastname, result.Lastname);
        }
    }
}
