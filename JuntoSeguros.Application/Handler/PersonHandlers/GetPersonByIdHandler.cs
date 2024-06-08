using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class GetPersonByIdHandler(ILogger<GetPersonByIdHandler> _logger, IActivityFactory _activityFactory, IPersonNSqlRepository _personRepository) 
    : IRequestHandler<GetPersonByIdCommand, PersonDto?>
{
    public async Task<PersonDto?> Handle(GetPersonByIdCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("GetPersonById-Handler");
        _logger.LogInformation("GetPersonById Person");

        return await Task.FromResult(_personRepository.GetMany(i => i.Id == request.PersonId).FirstOrDefault());
    }
}