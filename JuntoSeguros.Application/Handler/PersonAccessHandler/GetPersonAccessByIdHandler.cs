using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Handler.PersonAccessHandlers;

public class GetPersonAccessByIdHandler(ILogger<GetPersonAccessByIdHandler> _logger, IActivityFactory _activityFactory, IPersonAccessNSqlRepository _personRepository) 
    : IRequestHandler<GetPersonAccessByIdCommand, PersonAccessDto?>
{
    public async Task<PersonAccessDto?> Handle(GetPersonAccessByIdCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("GetPersonAccessById-Handler");
        _logger.LogInformation("GetPersonAccessById Person");

        return await Task.FromResult(_personRepository.GetMany(i => i.Id == request.PersonId).FirstOrDefault());
    }
}