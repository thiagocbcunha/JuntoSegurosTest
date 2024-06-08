using MediatR;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class EnableUserHandler(ILogger<EnableUserHandler> _logger, IActivityFactory _activityFactory, IPersonAccessRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<EnableUserCommand>
{
    public async Task Handle(EnableUserCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("EnableUser-Handler");

        _logger.LogInformation("Enable User Person");

        var person = (PersonAccess)request;
        person.Enable();

        await _personRepository.UpdateAsync(person);
        await _messagingSender.Send((PersonAccessDto)person);
    }
}