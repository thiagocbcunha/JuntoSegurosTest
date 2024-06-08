using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class GetAllPersonHandler(ILogger<GetAllPersonHandler> _logger, IActivityFactory _activityFactory, IPersonNSqlRepository _personRepository) : IRequestHandler<GetAllPersonCommand, IEnumerable<PersonDto>>
{
    public async Task<IEnumerable<PersonDto>> Handle(GetAllPersonCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("GetAll-Handler");
        _logger.LogInformation("GetAll Person");

        return await Task.FromResult(_personRepository.GetAll().ToList());
    }
}
