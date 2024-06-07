using JuntoSeguros.Domain.Entities;

namespace JuntoSeguros.Domain.Contracts;

public interface IPersonRepository: IRepository<Person, Guid>
{
}