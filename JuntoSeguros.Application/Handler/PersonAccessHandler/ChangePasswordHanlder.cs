using MediatR;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;
using JuntoSeguros.Domain.Exceptions;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Handler.PersonHandlers;

public class ChangePasswordHanlder(ILogger<ChangePasswordHanlder> _logger, IActivityFactory _activityFactory, IPersonAccessRepository _personRepository, IEnterpriseSecurity _enterpriseSecurity, IMessagingSender _messagingSender) : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        _activityFactory.Start("ChangePassword-Handler");

        _logger.LogInformation("Changing Password Person");

        var person = (PersonAccess)request;
        var pass = _enterpriseSecurity.GetHash(request.Password);
        var newPass = _enterpriseSecurity.GetHash(request.NewPassword);

        if (pass == newPass)
            throw new BusinessException("New password is the same.");

        person.ChangePassword(newPass);

        await _personRepository.UpdateAsync(person);
        await _messagingSender.Send((PersonAccessDto)person);
    }
}