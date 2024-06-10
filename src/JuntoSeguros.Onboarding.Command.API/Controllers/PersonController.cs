using MediatR;
using Microsoft.AspNetCore.Mvc;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Onboarding.Command.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController(ILogger<PersonController> logger, IMediator mediator, IActivityFactory activityFactory) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreatePersonCommand personCommand)
        {
            var activity = activityFactory.Start($"Create-Person");
            activity.Tag?.SetTag("MathodName", "Post");
            logger.LogInformation(message: $"Executing: Person method:Post");

            await mediator.Send(personCommand);

            return StatusCode(200);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ChangeGenderPersonCommand personCommand)
        {
            var activity = activityFactory.Start($"Change-Person");
            activity.Tag?.SetTag("MathodName", "Put");
            logger.LogInformation(message: $"Executing: Person method:Put");

            await mediator.Send(personCommand);

            return StatusCode(200);
        }
    }
}