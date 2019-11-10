using System.Collections.Generic;
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
        public async Task<IActionResult> GetMatches()
        {
            var email = HttpContext.User.FindFirst("emails")?.Value;
            if (email == null) return Forbid();

            var currentUser = _commonService.GetPersonExistStatus(email);
            if (currentUser == null) return Forbid();

            var matches = _matchingService.GetMatches(currentUser);
            return Ok(_mapper.Map<List<Person>>(matches));
        }
    }
}