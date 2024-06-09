using Moq;
using AutoFixture;
using FluentAssertions;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;
using JuntoSeguros.Application.Command.PersonAccessCommands;
using JuntoSeguros.Application.Handler.PersonAccessHandlers;

namespace JuntoSeguros.Application.Test.PersonAccessHandlers;

public class GetAllPersonAccessHandlerTest
{
    Fixture _fixture = new();
    GetAllPersonAccessHandler _handler;

    Mock<IActivityFactory> _activityFactoryMock = new();
    Mock<IPersonAccessNSqlRepository> _personRepositoryMock = new();
    Mock<ILogger<GetAllPersonAccessHandler>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new GetAllPersonAccessHandler(_loggerMock.Object, _activityFactoryMock.Object, _personRepositoryMock.Object);
    }

    [TearDown]
    public void Down()
    {
        _loggerMock = new();
        _activityFactoryMock = new();
        _personRepositoryMock = new();
    }

    [Test]
    public async Task ShoudExecuteHandlerSuccessfully()
    {
        var personAccessList = _fixture.CreateMany<PersonAccessDto>();
        _personRepositoryMock.Setup(m => m.GetAll()).Returns(personAccessList);

        var result = await _handler.Handle(new GetAllPersonAccessCommand(), new CancellationToken());

        result.Should().HaveCount(personAccessList.Count());
        _personRepositoryMock.Verify(m => m.GetAll(), Times.Once);
        _activityFactoryMock.Verify(m => m.Start("GetAllPersonAccess-Handler"), Times.Once);
    }
}