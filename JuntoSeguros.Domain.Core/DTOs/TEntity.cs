namespace JuntoSeguros.Domain.Core.DTOs;

public abstract class TEntity<Type>
{
    Type Id { get; set; }
}
