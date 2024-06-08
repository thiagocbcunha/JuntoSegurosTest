using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Domain.Contracts;

public interface IMessagingSender
{
    Task Send<Type>(Type person);
}