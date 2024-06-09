using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using Microsoft.Extensions.Configuration;
using JuntoSeguros.Infra.Mongo.BaseRepository;

namespace JuntoSeguros.Infra.Mongo;

public class MongoPersonRepository(ILogger<MongoRepository<PersonDto>> logger, IConfiguration configuration) 
    : MongoRepository<PersonDto>(logger, configuration, "Person"), IPersonNSqlRepository
{
}