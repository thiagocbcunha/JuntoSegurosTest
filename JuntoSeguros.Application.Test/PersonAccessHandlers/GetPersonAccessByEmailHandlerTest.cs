using AutoFixture;
using JuntoSeguros.Application.Command.PersonAccessCommands;
using JuntoSeguros.Application.Handler.PersonAccessHandlers;
using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using JuntoSeguros.Domain.Dtos;
using Microsoft.Extensions.Logging;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Enterprise.Library.Contracts;

namespace JuntoSeguros.Application.Test.PersonAccessHandlers;

public class GetPersonAccessByEmailHandlerTest
{
    Fixture _fixture = new();
    GetPersonAccessByEmailHandler _handler;

    Mock<IActivityFactory> _activityFactoryMock = new();
    Mock<IPersonAccessNSqlRepository> _personRepositoryMock = new();
    Mock<ILogger<GetPersonAccessByEmailHandler>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _handler = new GetPersonAccessByEmailHandler(_loggerMock.Object, _activityFactoryMock.Object, _personRepositoryMock.Object);
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
        var email = "test@teste.com.br";
        var personAccessList = _fixture.Build<PersonAccessDto>().With(i => i.Email, email).CreateMany();
        _personRepositoryMock.Setup(m => m.GetMany(It.IsAny<Expression<Func<PersonAccessDto, bool>>>())).Returns(personAccessList);

        var result = await _handler.Handle(new GetPersonAccessByEmailCommand(email), new CancellationToken());

        result.Should().NotBeNull();
        _activityFactoryMock.Verify(m => m.Start("GetPersonAccessByEmail-Handler"), Times.Once);
        _personRepositoryMock.Verify(m => m.GetMany(It.IsAny<Expression<Func<PersonAccessDto, bool>>>()), Times.Once);
    }
}