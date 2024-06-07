using Dapper;
using System.Data;
using JuntoSeguros.Infra.Contracts;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Infra.Repositories;

public class PersonRepository(IConnectionFactory connectionFactory) : IPersonRepository
{
    public async Task AddAsync(Person person)
    {
        string sqlPerson = "INSERT INTO[dbo].[Person] ([Name], [BirthDate]) OUTPUT INSERTED.Id VALUES (@Name, @BirthDate);";

        using (var connection = connectionFactory.Connection())
        {
            connection.Open();
            var personId = await connection.QuerySingleAsync<Guid>(sqlPerson, person);
            person.SetId(personId);
            await InsertEvent(person, connection);
        }
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        using (var connection = connectionFactory.Connection())
        {
            string sqlPerson =
                $"SELECT p.*, pe.GenderId, g.* " +
                "FROM Person p " +
                "INNER JOIN PersonEvent pe ON pe.PersonId = p.Id " +
                "AND pe.VersionNum = (SELECT MAX(VersionNum) FROM PersonEvent WHERE PersonId = p.Id) " +
                "INNER JOIN Gender g ON g.Id = pe.GenderId " +
                "WHERE p.Id = @Id";

            var person = await connection.QueryAsync<Person, Gender, Person>(sqlPerson, (person, gender) =>
                {
                    person.SetGender(gender);
                    return person;
                },
                param: new { Id = id },
                splitOn: "GenderId");

            return person.FirstOrDefault();
        }
    }

    public async Task UpdateAsync(Person person)
    {
        using (var connection = connectionFactory.Connection())
        {
            connection.Open();
            await InsertEvent(person, connection);
        }
    }

    private async Task InsertEvent(Person entity, IDbConnection connection)
    {
        string sqlPersonEvent = $"DECLARE @NewVersion INT = (SELECT ISNULL(MAX(VersionNum), 0) + 1 FROM PersonEvent WHERE PersonId=@PersonId); " +
                                $"INSERT INTO [dbo].[PersonEvent] ([PersonId] ,[VersionNum] ,[GenderId], [CreateBy], [CreateDate]) VALUES (@PersonId, @NewVersion, @GenderId, @SysName, getdate())";

        var eventObject = new
        {
            PersonId = entity.Id,
            GenderId = entity.Gender.Id,
            SysName = "JuntoSeguros.Onbording"
        };

        await connection.QueryAsync(sqlPersonEvent, eventObject);
    }
}
