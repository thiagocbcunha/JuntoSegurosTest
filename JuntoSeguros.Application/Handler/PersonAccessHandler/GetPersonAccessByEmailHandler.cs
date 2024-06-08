using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Handler.PersonAccessHandlers;

public class GetPersonAccessByEmailHandler(ILogger<GetPersonAccessByEmailHandler> _logger, IActivityFactory _activityFactory, IPersonAccessNSqlRepository _personRepository) 
    : IRequestHandler<GetPersonAccessByEmailCommand, PersonAccessDto?>
{
    public async Task<PersonAccessDto?> Handle(GetPersonAccessByEmailCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("GetPersonAccessByEmail-Handler");
        _logger.LogInformation("GetPersonAccessByEmail PersonAccess");

        return await Task.FromResult(_personRepository.GetMany(i => i.Email == request.Email).FirstOrDefault());
    }
}