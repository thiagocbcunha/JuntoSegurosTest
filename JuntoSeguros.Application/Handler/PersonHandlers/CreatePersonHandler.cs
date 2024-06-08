using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class CreatePersonHandler(ILogger<CreatePersonHandler> _logger, IActivityFactory _activityFactory, IPersonRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<CreatePersonCommand>
{
    public async Task Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("InsertPerson-Handler");
        _logger.LogInformation("Inserting Person");

        var person = (Person)request;
        await _personRepository.AddAsync(person);
        await _messagingSender.Send((PersonDto)person);
    }
}
