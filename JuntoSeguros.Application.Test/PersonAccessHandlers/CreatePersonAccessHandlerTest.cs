using Moq;
using AutoFixture;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Handler.PersonHandlers;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Test.PersonAccessHandlers;

public class CreatePersonAccessHandlerTest
{
    Fixture _fixture = new();
    CreatePersonAccessHandler _handler;

    Mock<IActivityFactory> _activityFactoryMock = new();
    Mock<IMessagingSender> _messagingSenderMock = new();
    Mock<IEnterpriseSecurity> _enterpriseSecurityMock = new();
    Mock<IPersonAccessRepository> _personRepositoryMock = new();
    Mock<ILogger<CreatePersonAccessHandler>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new CreatePersonAccessHandler(_loggerMock.Object, _activityFactoryMock.Object, _enterpriseSecurityMock.Object, _personRepositoryMock.Object, _messagingSenderMock.Object);
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
        var changeCommand = new CreatePersonAccessCommand(Guid.NewGuid(), "teste@teste.com.br", "123456789", DateTime.Now, true);
        await _handler.Handle(changeCommand, new CancellationToken());

        _activityFactoryMock.Verify(m => m.Start("NewUser-Handler"), Times.Once);
        _messagingSenderMock.Verify(m => m.Send(It.IsAny<PersonAccessDto>()), Times.Once);
        _personRepositoryMock.Verify(m => m.AddAsync(It.IsAny<PersonAccess>()), Times.Once);
    }
}