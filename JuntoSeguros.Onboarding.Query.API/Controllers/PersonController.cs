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
        [HttpGet(Name = "GetAll")]
        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            var activity = activityFactory.Start("GetInt");

            activity.Tag?.SetTag("Get", "Executing");

            logger.LogInformation(message: "Executing Get");

            var result = await mediator.Send(new GetAllPersonCommand());

            logger.LogInformation(message: "Executed Get");

            if (activity is not null)
                activity.Dispose();

            return result;
        }
    }
}