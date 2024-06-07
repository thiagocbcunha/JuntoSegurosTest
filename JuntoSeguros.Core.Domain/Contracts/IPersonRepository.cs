using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Domain.Contracts;

public interface IPersonRepository: IRepository<Person, Guid>
{
}