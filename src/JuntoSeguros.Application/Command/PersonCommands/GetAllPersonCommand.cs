using JuntoSeguros.Domain.Dtos;
using MediatR;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class GetAllPersonCommand : IRequest<IEnumerable<PersonDto>>
{
}
