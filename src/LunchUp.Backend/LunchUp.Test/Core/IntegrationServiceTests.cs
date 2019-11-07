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
            //Assert.Equal(0, LunchUpContext.Person.Count());
            // Act
            _integrationService.CreateOrUpdatePersons(new List<PersonEntity> { Entity }).Wait();
            
            // Assert
            var result = LunchUpContext.Person.FirstOrDefault(p => p.Email == Entity.Email);
            Assert.NotNull(result);
            Assert.Equal(Entity, result);
        }

        [Fact]
        public void GivenExistingPersonEntity_WhenCreateOrUpdatePerson_ThenPersonEntityUpdated()
        {
            // Arrange
            var personEntity = new PersonEntity(){ Firstname = "Peter", Lastname = "Meier", Email = Entity.Email};
            LunchUpContext.Person.Add(Entity);
            LunchUpContext.SaveChanges();

            // Act
            _integrationService.CreateOrUpdatePersons(new List<PersonEntity> { personEntity }).Wait();

            // Assert
            var result = LunchUpContext.Person.FirstOrDefault(p => p.Email == Entity.Email);
            Assert.NotNull(result);
            Assert.Equal(personEntity.Firstname, result.Firstname);
            Assert.Equal(personEntity.Lastname, result.Lastname);
        }
    }
}
