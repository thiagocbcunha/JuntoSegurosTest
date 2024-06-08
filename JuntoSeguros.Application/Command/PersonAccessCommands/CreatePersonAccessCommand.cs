namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class CreatePersonAccessCommand(Guid personId, string Email, string password, DateTime CreateDate, bool actived) : BaseCommand(personId, Email, password, CreateDate, actived)
{
}