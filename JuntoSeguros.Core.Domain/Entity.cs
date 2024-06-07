namespace JuntoSeguros.Domain;

public class Entity<TType>
{
    public TType Id { get; private set; }

    public void SetId(TType id) => Id = id;
}
