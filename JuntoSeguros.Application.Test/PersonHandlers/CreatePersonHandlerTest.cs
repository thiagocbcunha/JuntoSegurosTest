using Moq;
using AutoFixture;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Enums;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonCommands;
using JuntoSeguros.Application.Handler.PersonHandlers;

namespace JuntoSeguros.Application.Test.PersonHandlers;

public class CreatePersonHandlerTest
{
    Fixture _fixture = new();
    Mock<IActivityFactory> _activityFactoryMock = new();
    Mock<IMessagingSender> _messagingSenderMock = new();
    Mock<IPersonRepository> _personRepositoryMock = new();
    Mock<ILogger<CreatePersonHandler>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
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
        var insertCommand = new CreatePersonCommand(_fixture.Create<string>(), _fixture.Create<string>(), DateTime.Now, GenderEnum.Female);
        _personRepositoryMock.Setup(i => i.AddAsync(It.IsAny<Person>()));

        var handler = new CreatePersonHandler(_loggerMock.Object, _activityFactoryMock.Object, _personRepositoryMock.Object, _messagingSenderMock.Object);
        await handler.Handle(insertCommand, new CancellationToken());

        _messagingSenderMock.Verify(m => m.Send(It.IsAny<PersonDto>()), Times.Once);
        _activityFactoryMock.Verify(m => m.Start("InsertPerson-Handler"), Times.Once);
        _personRepositoryMock.Verify(m => m.AddAsync(It.IsAny<Person>()), Times.Once);
    }
}