using System;
using System.Collections.Generic;
using System.Text;
using LunchUp.Core.Common;
using LunchUp.Model;
using LunchUp.Model.Models;
using LunchUp.WebHost.Dto;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LunchUp.Test.Core
{
    public class CommonServiceTests
    {
        private readonly ICommonService _commonService;
        private readonly LunchUpContext _lunchUpContext;
        public CommonServiceTests()
        {
            var builder = new DbContextOptionsBuilder<LunchUpContext>()
                .UseInMemoryDatabase("lunchup");
            DbContextOptions<LunchUpContext> options = builder.Options;

            _lunchUpContext = new LunchUpContext(options);
            _lunchUpContext.Database.EnsureDeleted();
            _lunchUpContext.Database.EnsureCreated();
            _commonService = new CommonService(_lunchUpContext);
        }

        [Fact]
        public void GivenPersonEntityInDatabase_WhenGetPersonExistStatus_ThenPersonEntityReturned()
        {
            // Arrange
            var person = new PersonEntity() {Firstname = "John", Lastname = "Doe", Email = "john.doe@anonymous.com"};
            _lunchUpContext.Person.Add(person);
            _lunchUpContext.SaveChanges();

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
