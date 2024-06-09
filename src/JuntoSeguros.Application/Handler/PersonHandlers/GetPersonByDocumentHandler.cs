using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class GetPersonByDocumentHandler(ILogger<GetPersonByDocumentHandler> _logger, IActivityFactory _activityFactory, IPersonNSqlRepository _personRepository) 
    : IRequestHandler<GetPersonByDocumentCommand, PersonDto?>
{
    public async Task<PersonDto?> Handle(GetPersonByDocumentCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("GetPersonByDocument-Handler");
        _logger.LogInformation("GetPersonByDocument Person");

        return await Task.FromResult(_personRepository.GetMany(i => i.DocumentNumber == request.Document).FirstOrDefault());
    }
}