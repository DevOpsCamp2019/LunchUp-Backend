using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using LunchUp.Core;
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
        /// <returns>List of person</returns>
        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Person>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<List<Person>> GetSuggestions([FromQuery]int count = 10)
        {
            var suggestions = _matchingService.GetSuggestions();
            return Task.FromResult(_mapper.Map<List<Person>>(suggestions));
        }

        /// <summary>
        /// Save match
        /// </summary>
        /// <param name="personId">ID of the matching target Person</param>
        /// <param name="response"></param>
        [HttpPost]
        [Route("{personId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task CreateResponse([FromRoute][Required] Guid personId, [Required] Response response)
        {
            return Task.FromResult(StatusCodes.Status201Created);
        }
    }
}