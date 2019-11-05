using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LunchUp.WebHost.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchUp.WebHost.Controller
{
    /// <inheritdoc />
    [Route("api/integration")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        /// <inheritdoc />
        public IntegrationController() {  }

        /// <summary>
        /// Create a person
        /// </summary>
        [HttpPut]
        [Route("person")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task CreateOrUpdatePerson([FromBody][Required] Person person)
        {
            return Task.FromResult(StatusCodes.Status200OK);
        }

    }
}