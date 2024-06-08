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
        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Post(CreatePersonCommand personCommand)
        {
            var activity = activityFactory.Start("Post-Create");

            activity.Tag?.SetTag("Create", "Executing");

            logger.LogInformation(message: "Executing Create");

            await mediator.Send(personCommand);

            logger.LogInformation(message: "Create Executed");

            if (activity is not null)
                activity.Dispose();

            return StatusCode(200);
        }
    }
}