using JuntoSeguros.Domain.Dtos;
using MediatR;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class GetPersonAccessByIdCommand(Guid id) : IRequest<PersonAccessDto?>
{
    public Guid PersonId => id;
}