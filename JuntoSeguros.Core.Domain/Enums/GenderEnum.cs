using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Domain.Enums;

public enum GenderEnum: byte
{
    Female = 1,
    Male = 2,
    Other = 3
}

public static class GenderEnemExtension
{
    public static Gender GetGender(this GenderEnum gender)
    {
        return gender switch
        {
            GenderEnum.Female => new Gender((int)gender, "Feminino"),
            GenderEnum.Male => new Gender((int)gender, "Masculino"),
            _ => new Gender((int)gender, "Outros")
        };
    }
}