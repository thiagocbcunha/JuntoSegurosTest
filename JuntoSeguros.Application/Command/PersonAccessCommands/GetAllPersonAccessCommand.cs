using JuntoSeguros.Domain.Dtos;
using MediatR;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class GetAllPersonAccessCommand : IRequest<IEnumerable<PersonAccessDto>>
{
}
