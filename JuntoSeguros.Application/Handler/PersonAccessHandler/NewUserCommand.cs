using MediatR;
using JuntoSeguros.Domain.Contracts;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class NeUserHandler(ILogger<EnableUserHandler> _logger, IActivityFactory _activityFactory, IPersonAccessRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<NewUserCommand>
{
    public async Task Handle(NewUserCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("NewUser-Handler");

        _logger.LogInformation("Add New User Person");

        var person = (PersonAccess)request;

        await _personRepository.AddAsync(person);
        await _messagingSender.Send(person);
    }
}