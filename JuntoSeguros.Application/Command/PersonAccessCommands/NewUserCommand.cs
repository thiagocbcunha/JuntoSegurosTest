namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class NewUserCommand(Guid personId, string Email, string password, DateTime lastChange, bool actived) : BaseCommand(personId, Email, password, lastChange, actived)
{
}