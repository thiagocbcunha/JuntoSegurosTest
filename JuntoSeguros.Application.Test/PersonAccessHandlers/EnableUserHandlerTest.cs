using Moq;
using AutoFixture;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Handler.PersonHandlers;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Test.PersonHandlers;

public class EnableUserHandlerTest
{
    Fixture _fixture = new();
    EnablePersonAccessHandler _handler;

    Mock<IActivityFactory> _activityFactoryMock = new();
    Mock<IMessagingSender> _messagingSenderMock = new();
    Mock<ILogger<EnablePersonAccessHandler>> _loggerMock = new();
    Mock<IPersonAccessRepository> _personRepositoryMock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new EnablePersonAccessHandler(_loggerMock.Object, _activityFactoryMock.Object, _personRepositoryMock.Object, _messagingSenderMock.Object);
        _personRepositoryMock.Setup(i => i.UpdateAsync(It.IsAny<PersonAccess>()));
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
        var changeCommand = new EnablePersonAccessCommand(Guid.NewGuid(), _fixture.Create<string>(), _fixture.Create<string>(), DateTime.Now, true);
        await _handler.Handle(changeCommand, new CancellationToken());

        _activityFactoryMock.Verify(m => m.Start("EnableUser-Handler"), Times.Once);
        _messagingSenderMock.Verify(m => m.Send(It.IsAny<PersonDto>()), Times.Once);
        _personRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<PersonAccess>()), Times.Once);
    }
}