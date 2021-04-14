using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using State_Management_Models;

namespace State_Management_Dapr_Web_Api.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonApiController : ControllerBase
    {
        public const string StoreName = "statestore";
        private readonly DaprClient client;
        private readonly ILogger<PersonApiController> logger;

        public PersonApiController(DaprClient client, ILogger<PersonApiController> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        [HttpGet("byobject/{person}")]
        public ActionResult<Person> Get([FromState(StoreName)] StateEntry<Person> person)
        {
            if (person.Value is null) return NotFound();
            return person.Value;
        }
        
        [HttpGet("byemail/{email}")]
        [Produces(typeof(Person))]
        public async Task<ActionResult<Person>> GetByEmail(string email)
        {
            try
            {
                logger.LogInformation($"Getting data for specific {email}");
                var person = await client.GetStateAsync<Person>(StoreName, email);
                logger.LogInformation($"Data received: {person.FullName}");
                return person;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return NotFound();
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<Person>> SavePerson(Person person)
        {
            logger.LogInformation($"Saving person {person.FullName}");
            try
            {
                await client.SaveStateAsync(StoreName, person.Email, person);
                logger.LogInformation("Person was saved");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok();
        }
    }
}