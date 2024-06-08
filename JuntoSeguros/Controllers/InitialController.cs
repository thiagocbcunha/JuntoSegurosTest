using Microsoft.AspNetCore.Mvc;
using JuntoSeguros.Enterprise.Library.Contracts;

namespace JuntoSeguros.Onboarding.Query.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitialController(ILogger<InitialController> logger, IActivityFactory activityFactory) : ControllerBase
    {
        [HttpGet(Name = "GetInt")]
        public IEnumerable<int> Get()
        {
            var activity = activityFactory.Start("GetInt");

            activity.Tag?.SetTag("Get", "Executing");

            logger.LogInformation(message: "Executing Get");

            var result = Enumerable.Range(1, 5);

            logger.LogInformation(message: "Executed Get");

            if (activity is not null)
                activity.Dispose();

            return result;
        }
    }
}