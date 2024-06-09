using MediatR;
using JuntoSeguros.Domain.Dtos;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class GetPersonAccessByEmailCommand(string email) : IRequest<PersonAccessDto?>
{
    public string Email => Email;
}