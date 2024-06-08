using MediatR;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Domain.Enums;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class GenderGenderPersonHandler(ILogger<CreatePersonHandler> _logger, IActivityFactory _activityFactory, IPersonRepository _personRepository, IMessagingSender _messagingSender) : IRequestHandler<ChangeGenderPersonCommand>
{
    public async Task Handle(ChangeGenderPersonCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("ChangeGenderPerson-Handler");
        _logger.LogInformation("Changing Gender Person");

        var person = (Person)request;

        person.SetGender(GetGender(request.Gender));

        await _personRepository.UpdateAsync(person);
        await _messagingSender.Send((PersonDto)person);
    }

    private Gender GetGender(GenderEnum gender)
    {
        return gender switch
        {
            GenderEnum.Female => new Gender((int)gender, "Feminino"),
            GenderEnum.Male => new Gender((int)gender, "Masculino"),
            _ => new Gender((int)gender, "Outros")
        };
    }
}