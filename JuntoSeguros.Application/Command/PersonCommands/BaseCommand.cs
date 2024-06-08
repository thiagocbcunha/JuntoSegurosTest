using MediatR;
using JuntoSeguros.Domain.Enums;
using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class BaseCommand(string name, DateTime birthDate, GenderEnum gender) : IRequest
{
    public string Name { get; set; } = name;
    public GenderEnum Gender { get; set; } = gender;
    public DateTime BirthDate { get; set; } = birthDate;

    public static explicit operator Person(BaseCommand command)
    {
        var person = new Person()
        {
            Name = command.Name,
            BirthDate = command.BirthDate
        };

        person.SetGender(new Gender((int)command.Gender, ""));

        return person;
    }
}
