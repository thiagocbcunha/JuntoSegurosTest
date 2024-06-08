using JuntoSeguros.Domain.Dtos;

namespace JuntoSeguros.Domain.Contracts;

public interface INSqlRepository<TMongoEntity> : INSqlViewRepository<TMongoEntity>
    where TMongoEntity : MongoEntity
{
    void DeleteAll();
    void Delete(TMongoEntity register);
    void Update(TMongoEntity register);
    void Insert(TMongoEntity register);
    void InsertMany(IEnumerable<TMongoEntity> registers);
}