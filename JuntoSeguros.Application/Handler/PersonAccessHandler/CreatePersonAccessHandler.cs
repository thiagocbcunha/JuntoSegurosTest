using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class CreatePersonAccessHandler(ILogger<CreatePersonAccessHandler> _logger, IActivityFactory _activityFactory, IEnterpriseSecurity _enterpriseSecurity, IPersonAccessRepository _personRepository, IMessagingSender _messagingSender) 
    : IRequestHandler<CreatePersonAccessCommand>
{
    public async Task Handle(CreatePersonAccessCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("NewUser-Handler");

        _logger.LogInformation("Add New User Person");

        var person = (PersonAccess)request;
        var encriptedPass = _enterpriseSecurity.GetHash(request.Password);

        person.ChangePassword(encriptedPass);
        await _personRepository.AddAsync(person);
        await _messagingSender.Send((PersonAccessDto)person);
    }
}