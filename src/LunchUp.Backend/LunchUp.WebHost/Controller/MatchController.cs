using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    [Route("api/match")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        [HttpGet]
        public Task<List<Person>> GetMatches()
        {
            var matches = SampleData.Suggestions().Take(2).ToList();
            return Task.FromResult(matches);
        }
    }
}