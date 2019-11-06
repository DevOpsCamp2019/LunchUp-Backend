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
        public IntegrationController() {  }

        /// <summary>
        /// Create or update a person
        /// </summary>
        /// <response code="200">Person updated</response>
        /// <response code="201">Person created</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [Route("person")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task CreateOrUpdatePerson([FromBody][Required] Person person)
        {
            var personEntity = _mapper.Map<PersonEntity>(person);
            _integrationService.CreateOrUpdatePerson(personEntity);
            return Task.FromResult(StatusCodes.Status201Created);
        }
    }
}