using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    [Route("api/suggestion")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public Task<List<Person>> GetSuggestions()
        {
            var suggestions = SampleData.Suggestions();
            return Task.FromResult(suggestions);
        }

        [HttpPost]
        [Route("{id}")]
        public void CreateReponse([FromRoute] Guid personId)
        {
        }
    }
}