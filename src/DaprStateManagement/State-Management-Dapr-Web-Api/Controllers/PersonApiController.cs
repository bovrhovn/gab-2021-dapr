using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using State_Management_Dapr_Web_Api.Settings;
using State_Management_Models;

namespace State_Management_Dapr_Web_Api.Controllers
{
    /// <summary>
    /// runs API controller, which can be accessed by people
    /// </summary>
    /// <example>
    ///     dapr run --app-id state-management-web-api --dapr-grpc-port 50001 --app-port 5002 
    /// </example>
    [ApiController]
    [Route("persons")]
    public class PersonApiController : ControllerBase
    {
        private readonly string StoreName;
        private readonly DaprClient client;
        private readonly ILogger<PersonApiController> logger;

        public PersonApiController(DaprClient client, ILogger<PersonApiController> logger, IOptions<WebSettings> webSettingsValue)
        {
            this.client = client;
            this.logger = logger;
            StoreName = webSettingsValue.Value.StoreStateName;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult IsEndpointOk() => Ok($"I am ok at {DateTime.Now}");

        [HttpGet("byobject/{person}")]
        public ActionResult<Person> Get([FromState("statestore")] StateEntry<Person> person)
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
                if (person == null)
                {
                    logger.LogInformation("No data was found in the system");
                    return Ok(new Person());
                }
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
                logger.LogInformation($"Person {person.FullName} was saved");
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