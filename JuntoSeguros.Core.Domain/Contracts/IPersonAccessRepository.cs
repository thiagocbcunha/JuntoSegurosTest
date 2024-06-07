using JuntoSeguros.Domain.Entities.PersonAccessEntity;

namespace JuntoSeguros.Domain.Contracts;

public interface IPersonAccessRepository: IRepository<PersonAccess, Guid>
{
}