namespace JuntoSeguros.Infra.Queries;

internal class CommonQueries
{
    public static string newVersionSql = "DECLARE @NewVersion INT = (SELECT ISNULL(MAX(VersionNum), 0) + 1 FROM PersonEvent WHERE @Where);";
}
