using MediatR;
using Microsoft.AspNetCore.Mvc;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Onboarding.Command.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonAccessController(ILogger<PersonController> logger, IMediator mediator, IActivityFactory activityFactory) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreatePersonAccessCommand personCommand)
        {
            var activity = activityFactory.Start($"Create-PersonAccess");
            activity.Tag?.SetTag("MathodName", "Post");
            logger.LogInformation(message: $"Executing: PersonAccess method:CreatePersonAccess");

            await mediator.Send(personCommand);

            return StatusCode(200);
        }

        [HttpPut("change/password/{id}")]
        public async Task<IActionResult> Put([FromBody] ChangePasswordCommand personCommand)
        {
            var activity = activityFactory.Start($"ChangePassword-PersonAccess");
            activity.Tag?.SetTag("MathodName", "Put");
            logger.LogInformation(message: $"Executing: PersonAccess method:ChangePasswordPersonAccess");

            await mediator.Send(personCommand);

            return StatusCode(200);
        }

        [HttpPut("change/enable/{id}")]
        public async Task<IActionResult> Put([FromBody] EnablePersonAccessCommand personCommand)
        {
            var activity = activityFactory.Start($"Enable-PersonAccess");
            activity.Tag?.SetTag("MathodName", "Put");
            logger.LogInformation(message: $"Executing: PersonAccess method:EnablePersonAccess");

            await mediator.Send(personCommand);

            return StatusCode(200);
        }

        [HttpPut("change/disable/{id}")]
        public async Task<IActionResult> Put([FromBody] DisablePersonAccessCommand personCommand)
        {
            var activity = activityFactory.Start($"Disable-PersonAccess");
            activity.Tag?.SetTag("MathodName", "Put");
            logger.LogInformation(message: $"Executing: PersonAccess method:DisablePersonAccess");

            await mediator.Send(personCommand);

            return StatusCode(200);
        }
    }
}