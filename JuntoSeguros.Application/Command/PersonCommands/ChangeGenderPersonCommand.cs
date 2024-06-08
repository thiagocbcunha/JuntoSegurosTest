using JuntoSeguros.Domain.Enums;
using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class ChangeGenderPersonCommand(Guid id, string name, DateTime birthDate, GenderEnum gender) : BaseCommand(name, birthDate, gender)
{
    public Guid Id { get; } = id;

    public static explicit operator Person(ChangeGenderPersonCommand command)
    {
        var person = (Person)(BaseCommand)command;
        person.SetId(command.Id);
        return person;
    }
}