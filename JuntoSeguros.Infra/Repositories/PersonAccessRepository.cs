using Dapper;
using System.Data;
using JuntoSeguros.Infra.Contracts;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;

namespace JuntoSeguros.Infra.Repositories;

public class PersonAccessRepository(IConnectionFactory connectionFactory) : IPersonAccessRepository
{
    public async Task AddAsync(PersonAccess personAccess)
    {
        string sqlPerson = "INSERT INTO[dbo].[PersonAccess] ([PersonId], [UserName]) OUTPUT INSERTED.PersonId VALUES (@PersonId, @UserName);";

        using (var connection = connectionFactory.Connection())
        {
            connection.Open();
            var personId = await connection.QuerySingleAsync<Guid>(sqlPerson, personAccess);
            personAccess.SetId(personId);
            await InsertEvent(personAccess, connection);
        }
    }

    public async Task<PersonAccess?> GetByIdAsync(Guid id)
    {
        using (var connection = connectionFactory.Connection())
        {
            string sqlPerson =
                "SELECT TOP 1 pa.*, pav.Actived, pav.EncryptedPass, pav.CreateDate " +
                "FROM PersonAccess pa " +
                "INNER JOIN PersonAccessEvent pav ON pav.PersonId = pa.PersonId " +
                "AND pav.VersionNum = (SELECT MAX(VersionNum) FROM PersonAccessEvent WHERE PersonId = pa.PersonId) " +
                "WHERE pa.PersonId = @Id";

            var row = await connection.QueryFirstOrDefaultAsync<dynamic>(sqlPerson, new { Id = id });

            if (row is not null)
            {
                var result = new PersonAccess(row.UserName, row.Actived, row.EncryptedPass, row.CreateDate, false);
                result.SetId(id);

                return result;
            }

            return null;
        }
    }

    public async Task UpdateAsync(PersonAccess person)
    {
        using (var connection = connectionFactory.Connection())
        {
            connection.Open();
            await InsertEvent(person, connection);
        }
    }

    private async Task InsertEvent(PersonAccess entity, IDbConnection connection)
    {
        if (entity.Changed)
        {
            string sqlPersonEvent = "DECLARE @NewVersion INT = (SELECT ISNULL(MAX(VersionNum), 0) + 1 FROM PersonAccessEvent WHERE PersonId=@PersonId); " +
                                    "INSERT INTO [dbo].[PersonAccessEvent] ([PersonId],[VersionNum],[Actived],[EncryptedPass],[CreateBy],[CreateDate]) VALUES (@PersonId, @NewVersion ,@Actived, @EncryptedPass, @SysName, getdate())";

            var eventObject = new
            {
                PersonId = entity.Id,
                Actived = entity.Actived,
                EncryptedPass = entity.EncryptedPass,
                SysName = "JuntoSeguros.Onbording"
            };

            await connection.QueryAsync(sqlPersonEvent, eventObject);
        }
    }
}