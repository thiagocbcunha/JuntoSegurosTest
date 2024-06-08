using MediatR;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Enterprise.Library.Contracts;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class InsertPersonHandler(ILogger<InsertPersonHandler> _logger, IActivityFactory _activityFactory, IPersonRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<InsertPersonCommand>
{
    public async Task Handle(InsertPersonCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("InsertPerson-Handler");
        _logger.LogInformation("Inserting Person");

        var person = (Person)request;
        await _personRepository.AddAsync(person);
        await _messagingSender.Send(person);
    }
}
