using System.Linq.Expressions;

namespace JuntoSeguros.Domain.Contracts;

public interface INSqlViewRepository<TModel>
{
    IEnumerable<TModel> GetAll();
    IEnumerable<TModel> GetMany(Expression<Func<TModel, bool>> filter);
}