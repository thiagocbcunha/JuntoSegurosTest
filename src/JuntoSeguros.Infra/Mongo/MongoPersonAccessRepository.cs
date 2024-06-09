using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using Microsoft.Extensions.Configuration;
using JuntoSeguros.Infra.Mongo.BaseRepository;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;

namespace JuntoSeguros.Infra.Mongo;

public class MongoPersonAccessRepository(ILogger<MongoRepository<PersonAccessDto>> logger, IConfiguration configuration)
    : MongoRepository<PersonAccessDto>(logger, configuration, "PersonAccess"), IPersonAccessNSqlRepository
{
}