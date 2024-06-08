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
        public async Task<PersonDto?> Get(Guid id)
        {
            var activity = activityFactory.Start($"GetById");
            activity.Tag?.SetTag("MathodName", "Get");
            activity.Tag?.SetTag("Paramenter", id);
            logger.LogInformation(message: $"Executing: Person method:GetById");

            var result = await mediator.Send(new GetPersonByIdCommand(id));

            return result;
        }

        [HttpGet("/document/{document}")]
        public async Task<PersonDto?> Get(string document)
        {
            var activity = activityFactory.Start($"GetByDocument");
            activity.Tag?.SetTag("MathodName", "Get");
            activity.Tag?.SetTag("Paramenter", document);
            logger.LogInformation(message: $"Executing: Person method:GetById");

            var result = await mediator.Send(new GetPersonByDocumentCommand(document));

            return result;
        }
    }
}