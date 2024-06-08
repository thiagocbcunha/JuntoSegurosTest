using JuntoSeguros.Domain.Enums;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class CreatePersonCommand(string name, DateTime birthDate, GenderEnum gender) : BaseCommand(name, birthDate, gender)
{
}