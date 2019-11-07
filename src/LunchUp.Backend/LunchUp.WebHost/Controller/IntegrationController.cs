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
        public async Task<IActionResult> CreateOrUpdatePersons([FromBody] [Required] IEnumerable<Person> persons)
        {
            var entityList = _mapper.Map<IEnumerable<PersonEntity>>(persons);

            foreach (var personEntity in entityList)
            {
                await _integrationService.CreateOrUpdatePerson(personEntity);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}