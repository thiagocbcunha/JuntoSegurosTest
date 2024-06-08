namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class DisableUserCommand(Guid personId, string userName, string password, DateTime lastChange, bool actived) : BaseCommand(personId, userName, password, lastChange, actived)
{
}