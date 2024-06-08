using JuntoSeguros.Domain.Dtos;
using MediatR;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class GetPersonByIdCommand(Guid id) : IRequest<PersonDto?>
{
    public Guid PersonId => id;
}