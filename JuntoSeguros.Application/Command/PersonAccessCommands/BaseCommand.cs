﻿using MediatR;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;

namespace JuntoSeguros.Application.Command.PersonAccessCommands;

public class BaseCommand(Guid personId, string userName, string password, DateTime lastChange, bool actived) : IRequest
{
    public bool Actived { get; set; } = actived;
    public Guid PersonId { get; set; } = personId;
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
    public DateTime LastChange { get; set; } = lastChange;

    public static explicit operator PersonAccess(BaseCommand command)
    {
        return new PersonAccess(command.UserName, new PersonAccessEvent(command.Actived, command.Password, command.LastChange));
    }
}