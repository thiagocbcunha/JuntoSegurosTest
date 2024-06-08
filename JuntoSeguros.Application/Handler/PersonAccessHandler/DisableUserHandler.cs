using MediatR;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class DisableUserHandler(ILogger<DisableUserHandler> _logger, IActivityFactory _activityFactory, IPersonAccessRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<DisableUserCommand>
{
    public async Task Handle(DisableUserCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("DisableUser-Handler");

        _logger.LogInformation("Disable User Person");

        var person = (PersonAccess)request;
        person.Disable();

        await _personRepository.UpdateAsync(person);
        await _messagingSender.Send((PersonAccessDto)person);
    }
}