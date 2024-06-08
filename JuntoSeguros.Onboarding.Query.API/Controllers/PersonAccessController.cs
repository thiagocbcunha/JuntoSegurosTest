using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Onboarding.Query.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonAccessController(ILogger<PersonController> logger, IActivityFactory activityFactory, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<PersonAccessDto>> Get()
        {
            var activity = activityFactory.Start($"GetAll");
            activity.Tag?.SetTag("MathodName", "Get");
            logger.LogInformation(message: $"Executing: PersonAccess method:GetAll");

            var result = await mediator.Send(new GetAllPersonAccessCommand());

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<PersonAccessDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = activityFactory.Start($"GetById");
            activity.Tag?.SetTag("MathodName", "Get");
            activity.Tag?.SetTag("Paramenter", id);
            logger.LogInformation(message: $"Executing: PersonAccess method:GetById");

            var result = await mediator.Send(new GetPersonAccessByIdCommand(id));

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("/email/{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<PersonAccessDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string email)
        {
            var activity = activityFactory.Start($"GetByEmail");
            activity.Tag?.SetTag("MathodName", "Get");
            activity.Tag?.SetTag("Paramenter", email);
            logger.LogInformation(message: $"Executing: PersonAccess method:GetById");

            var result = await mediator.Send(new GetPersonAccessByEmailCommand(email));

            return result is null ? NotFound() : Ok(result);
        }
    }
}