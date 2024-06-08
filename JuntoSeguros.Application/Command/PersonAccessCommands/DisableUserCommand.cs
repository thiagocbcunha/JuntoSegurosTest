namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class DisableUserCommand(Guid personId, string Email, string password, DateTime lastChange, bool actived) : BaseCommand(personId, Email, password, lastChange, actived)
{
}