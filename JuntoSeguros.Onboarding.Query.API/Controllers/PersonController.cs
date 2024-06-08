using MediatR;
using Microsoft.AspNetCore.Mvc;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Domain.Dtos;

namespace JuntoSeguros.Onboarding.Query.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController(ILogger<PersonController> logger, IActivityFactory activityFactory, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<PersonDto>> Get()
        {
            var activity = activityFactory.Start($"GetAll");
            activity.Tag?.SetTag("MathodName", "Get");
            logger.LogInformation(message: $"Executing: Person method:GetAll"); ;

            var result = await mediator.Send(new GetAllPersonCommand());

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<PersonDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = activityFactory.Start($"GetById");
            activity.Tag?.SetTag("MathodName", "Get");
            activity.Tag?.SetTag("Paramenter", id);
            logger.LogInformation(message: $"Executing: Person method:GetById");

            var result = await mediator.Send(new GetPersonByIdCommand(id));

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("/document/{document}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<PersonDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string document)
        {
            var activity = activityFactory.Start($"GetByDocument");
            activity.Tag?.SetTag("MathodName", "Get");
            activity.Tag?.SetTag("Paramenter", document);
            logger.LogInformation(message: $"Executing: Person method:GetById");

            var result = await mediator.Send(new GetPersonByDocumentCommand(document));

            return result is null ? NotFound() : Ok(result);
        }
    }
}