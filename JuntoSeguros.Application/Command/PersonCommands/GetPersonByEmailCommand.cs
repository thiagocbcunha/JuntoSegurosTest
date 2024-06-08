using JuntoSeguros.Domain.Dtos;
using MediatR;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class GetPersonByDocumentCommand(string document) : IRequest<PersonDto?>
{
    public string Document => document;
}