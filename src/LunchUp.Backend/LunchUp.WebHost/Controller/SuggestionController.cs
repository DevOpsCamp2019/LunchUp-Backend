using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using LunchUp.Core.Matching;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    /// <inheritdoc />
    [Route("api/suggestion")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMatchingService _matchingService;

        /// <inheritdoc />
        public SuggestionController(IMapper mapper, IMatchingService matchingService)
        {
            _mapper = mapper;
            _matchingService = matchingService;
        }

        /// <summary>
        /// Get random suggestions
        /// </summary>
        /// <param name="count">Number of suggestions</param>
        /// <returns>List of persons</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<List<Person>> GetSuggestions([FromQuery] int count = 10)
        {
            var suggestions = _matchingService.GetSuggestions();
            return Task.FromResult(_mapper.Map<List<Person>>(suggestions));
        }

        /// <summary>
        /// Response to a suggestion
        /// </summary>
        /// <param name="personId">ID of the matching target Person</param>
        /// <param name="result"></param>
        /// <response code="200">Response Created</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Route("{personId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task CreateReponse([FromRoute] [Required] Guid personId, [FromBody] [Required] bool result)
        {
            return Task.FromResult(StatusCodes.Status201Created);
        }
    }
}