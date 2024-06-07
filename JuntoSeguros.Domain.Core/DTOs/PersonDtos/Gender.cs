namespace JuntoSeguros.Domain.Core.DTOs.PersonDtos;

public class Gender : TEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
}