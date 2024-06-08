using Moq;
using FluentAssertions;
using JuntoSeguros.Domain.Enums;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Application.Handler.PersonHandlers;
using AutoFixture;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Test;

public class GenderGenderPersonHandlerTest
{
    Fixture _fixture = new();
    ChangeGenderPersonHandler _handler;
    Mock<IActivityFactory> _activityFactoryMock = new();
    Mock<IMessagingSender> _messagingSenderMock = new();
    Mock<IPersonRepository> _personRepositoryMock = new();
    Mock<ILogger<CreatePersonHandler>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new ChangeGenderPersonHandler(_loggerMock.Object, _activityFactoryMock.Object, _personRepositoryMock.Object, _messagingSenderMock.Object);
        _personRepositoryMock.Setup(i => i.UpdateAsync(It.IsAny<Person>()));
    }

    [TearDown]
    public void Down()
    {
        _loggerMock = new();
        _activityFactoryMock = new();
        _messagingSenderMock = new();
        _personRepositoryMock = new();
    }

    [Test]
    public async Task ShoudExecuteHandlerSuccessfully()
    {
        var changeCommand = new ChangeGenderPersonCommand(Guid.NewGuid(), _fixture.Create<string>(), _fixture.Create<string>(), DateTime.Now, GenderEnum.Female);
        await _handler.Handle(changeCommand, new CancellationToken());

        _messagingSenderMock.Verify(m => m.Send(It.IsAny<PersonDto>()), Times.Once);
        _activityFactoryMock.Verify(m => m.Start("ChangeGenderPerson-Handler"), Times.Once);
        _personRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Person>()), Times.Once);
    }
}