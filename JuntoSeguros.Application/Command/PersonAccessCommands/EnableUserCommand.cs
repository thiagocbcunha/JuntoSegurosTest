namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class EnableUserCommand(Guid personId, string userName, string password, DateTime lastChange) : BaseCommand(personId, userName, password, lastChange)
{
}