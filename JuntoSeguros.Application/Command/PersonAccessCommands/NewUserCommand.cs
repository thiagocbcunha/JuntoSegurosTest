namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class NewUserCommand(Guid personId, string userName, string password, DateTime lastChange) : BaseCommand(personId, userName, password, lastChange)
{
}