using System;
using AutoMapper;
using LunchUp.Model.Models;
using LunchUp.WebHost.Dto;
using LunchUp.WebHost.Profiles;
using Xunit;

namespace LunchUp.Test.WebHost.Profiles
{
    public class PersonProfileTests
    {
        private readonly IMapper _mapper;

        public PersonProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PersonProfile>();
            });
            config.AssertConfigurationIsValid();
            _mapper = new Mapper(config);
        }

        [Fact]
        public void GivenValidPerson_WhenMapToPersonEntity_ThenValidPersonEntityReturned()
        {
            // Arrange
            var person = new Person
            {
                Id = Guid.NewGuid(), 
                Firstname = "John", 
                Lastname = "Doe", 
                Email = "john.doe@anonymous.com",
                Photo = "photo"
            };

            // Act
            var personEntity = _mapper.Map<PersonEntity>(person);

            // Assert
            Assert.Equal(person.Id, personEntity.Id);
            Assert.Equal(person.Firstname, personEntity.Firstname);
            Assert.Equal(person.Lastname, personEntity.Lastname);
            Assert.Equal(person.Email, personEntity.Email);
            Assert.Equal(person.Photo, personEntity.Photo);
        }

        [Fact]
        public void GivenValidPersonEntity_WhenMapToPerson_ThenValidPersonReturned()
        {
            // Arrange
            var personEntity = new PersonEntity
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@anonymous.com",
                Photo = "photo"
            };

            // Act
            var person = _mapper.Map<PersonEntity>(personEntity);

            // Assert
            Assert.Equal(personEntity.Id, person.Id);
            Assert.Equal(personEntity.Firstname, person.Firstname);
            Assert.Equal(personEntity.Lastname, person.Lastname);
            Assert.Equal(personEntity.Email, person.Email);
            Assert.Equal(personEntity.Photo, person.Photo);
        }
    }
}
