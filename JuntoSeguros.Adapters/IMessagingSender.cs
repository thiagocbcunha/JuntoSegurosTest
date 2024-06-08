
namespace JuntoSeguros.Adapters;

public interface IMessagingSender
{
    Task Send(JuntoSeguros.Domain.Entities.PersonEntity.Person person);
}