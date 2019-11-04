using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    [Route("api/suggestion")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        /// <summary>
        /// Get random suggestions
        /// </summary>
        /// <param name="number">Number of suggestions</param>
        /// <returns>List of person</returns>
        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public Task<List<Person>> GetSuggestions(int number = 10)
        {
            var suggestions = SampleData.Suggestions();
            return Task.FromResult(suggestions);
        }

        /// <summary>
        /// Save match
        /// </summary>
        /// <param name="personId">ID of the matching target Person</param>
        /// <param name="result"></param>
        [HttpPost]
        [Route("{personId}")]
        public void CreateReponse([FromRoute][Required] Guid personId,[Required] bool result)
        {
        }
    }
}