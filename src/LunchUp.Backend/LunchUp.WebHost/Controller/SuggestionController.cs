using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LunchUp.Core.Common;
using LunchUp.Core.Matching;
using LunchUp.Model.Models;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    /// <inheritdoc />
    [Route("api/suggestion")]
    [Authorize]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMatchingService _matchingService;
        private readonly ICommonService _commonService;

        /// <inheritdoc />
        public SuggestionController(IMapper mapper, IMatchingService matchingService, ICommonService commonService)
        {
            _mapper = mapper;
            _matchingService = matchingService;
            _commonService = commonService;
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
            var currentUserUpn = HttpContext.User.FindFirst("emails")?.Value;
            var userExists = _commonService.GetPersonExistStatus(currentUserUpn);
            if (!userExists) throw new ApplicationException("The user not exist");
            var suggestions = _matchingService.GetSuggestions(currentUserUpn, count);
            return Task.FromResult(_mapper.Map<List<Person>>(suggestions));
        }

        /// <summary>
        /// Response to a suggestion
        /// </summary>
        /// <param name="personId">ID of the matching target Person</param>
        /// <param name="response"></param>
        /// <response code="200">Response Created</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Route("{personId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task CreateResponse([FromRoute] [Required] Guid personId, [FromBody] [Required] Response response)
        {
            var currentUserUpn = HttpContext.User.FindFirst("emails")?.Value;
            var userExists = _commonService.GetPersonExistStatus(currentUserUpn);
            if (!userExists) throw new ApplicationException("The user not exist");
            _matchingService.AddMatch(currentUserUpn, personId, response.Accepted);
            return Task.FromResult(StatusCodes.Status201Created);
        }
    }
}