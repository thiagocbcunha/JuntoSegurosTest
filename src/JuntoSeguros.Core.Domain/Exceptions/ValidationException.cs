namespace JuntoSeguros.Domain.Exceptions;

public class ValidationException(IList<string> errors) : Exception(string.Join(", ", errors))
{
    public IList<string> Errors { get; set; } = errors;
}