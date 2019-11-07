using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using LunchUp.Core.Integration;
using LunchUp.Model.Models;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    /// <inheritdoc />
    [Route("api/integration")]
    [Authorize]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationService _integrationService;
        private readonly IMapper _mapper;


        /// <inheritdoc />
        public IntegrationController(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }
        
        /// <summary>
        ///     Create or update a person
        /// </summary>
        /// <response code="200">Person updated</response>
        /// <response code="201">Person created</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [Route("person")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task CreateOrUpdatePersons([FromBody] [Required] IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                var personEntity = new PersonEntity();
                personEntity.Email = person.Email;
                personEntity.Firstname = person.Firstname;
                personEntity.Lastname = person.Lastname;
                personEntity.Photo = person.Photo;
                personEntity.Id = person.Id;
                _integrationService.CreateOrUpdatePerson(personEntity);
            }

            return Task.FromResult(StatusCodes.Status201Created);
        }
    }
}