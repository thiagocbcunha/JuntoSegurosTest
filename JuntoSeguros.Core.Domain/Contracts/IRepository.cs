namespace JuntoSeguros.Domain.Contracts;

public interface IRepository<TEntity, in TType>
    where TEntity : Entity<TType>
{
    Task UpdateAsync(TEntity person);
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(TType id);

}