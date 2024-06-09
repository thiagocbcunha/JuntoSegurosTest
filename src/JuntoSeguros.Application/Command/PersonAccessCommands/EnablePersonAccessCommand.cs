namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class EnablePersonAccessCommand(Guid personId, string Email, string password, DateTime CreateDate, bool actived) : BaseCommand(personId, Email, password, CreateDate, actived)
{
}