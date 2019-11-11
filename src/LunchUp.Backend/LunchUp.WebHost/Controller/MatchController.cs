using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LunchUp.Core.Common;
using LunchUp.Core.Matching;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    /// <inheritdoc />
    [Route("api/match")]
    [Authorize]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMatchingService _matchingService;
        private readonly ICommonService _commonService;

        /// <inheritdoc />
        public MatchController(IMapper mapper, IMatchingService matchingService, ICommonService commonService)
        {
            _mapper = mapper;
            _matchingService = matchingService;
            _commonService = commonService;
        }

        /// <summary>
        ///     Get all matches
        /// </summary>
        /// <returns>A list of matches</returns>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Person>), StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public Task<List<Person>> GetMatches()
        {
            var currentUser = _commonService.GetPersonExistStatus(HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);
            if (currentUser == null) Task.FromResult(StatusCodes.Status403Forbidden);
            var matches = _matchingService.GetMatches(currentUser);
            return Task.FromResult(_mapper.Map<List<Person>>(matches));
        }
    }
}