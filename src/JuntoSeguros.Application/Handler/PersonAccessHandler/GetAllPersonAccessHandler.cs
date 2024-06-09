using MediatR;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Handler.PersonAccessHandlers;

public class GetAllPersonAccessHandler(ILogger<GetAllPersonAccessHandler> _logger, IActivityFactory _activityFactory, IPersonAccessNSqlRepository _personRepository) 
    : IRequestHandler<GetAllPersonAccessCommand, IEnumerable<PersonAccessDto>>
{
    public async Task<IEnumerable<PersonAccessDto>> Handle(GetAllPersonAccessCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("GetAllPersonAccess-Handler");
        _logger.LogInformation("GetAllPersonAccess Person");

        return await Task.FromResult(_personRepository.GetAll().ToList());
    }
}
