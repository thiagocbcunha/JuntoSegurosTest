using MediatR;
using JuntoSeguros.Domain.Contracts;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class GenderGenderPersonHandler(ILogger<InsertPersonHandler> _logger, IActivityFactory _activityFactory, IPersonRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<ChangeGenderPersonCommand>
{
    public async Task Handle(ChangeGenderPersonCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("ChangeGenderPerson-Handler");
        _logger.LogInformation("Changing Gender Person");

        var person = (Person)request;
        await _personRepository.UpdateAsync(person);
        await _messagingSender.Send(person);
    }
}