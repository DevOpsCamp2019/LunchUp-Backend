using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public Task<List<Person>> GetMatches()
        {
            var matches = SampleData.Suggestions().Take(2).ToList();
            return Task.FromResult(matches);
        }
    }
}