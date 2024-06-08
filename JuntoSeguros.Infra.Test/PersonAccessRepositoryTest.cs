using AutoFixture;
using FluentAssertions;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Infra.Dapper.Connection;
using JuntoSeguros.Infra.Dapper.Contracts;
using JuntoSeguros.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;

namespace JuntoSeguros.Infra.Test
{
    public class PersonAccessRepositoryTest
    {
        private Fixture _fixture = new();
        private IPersonRepository _prepository;
        private IPersonAccessRepository _repository;
        private IConnectionFactory _connectionFactory;

        [SetUp]
        public void Setup()
        {
            var connectionString = new Dictionary<string, string>
            {
                {"ConnectionStrings:JuntoSegurosOnboarding", "Server=127.0.0.1,1433; Database=JuntoSegurosOnboarding; User Id=sa;Password=SqlServer2022!;"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(initialData: connectionString)
                .Build();

            _connectionFactory = new DapperConnectionFactory(configuration);
            _prepository = new PersonRepository(_connectionFactory);
            _repository = new PersonAccessRepository(_connectionFactory);
        }

        [Test]
        public async Task ShouldBeAddNewPersonAccessSuccessfully()
        {
            var person = _fixture.Create<Person>();
            person.SetGender(new Gender(1, "Feminino"));
            await _prepository.AddAsync(person);

            var personAccess = new PersonAccess("TESTE", new PersonAccessEvent(true, "123", DateTime.Now.AddDays(-50)));
            personAccess.SetId(person.Id);
            personAccess.Enable();
            personAccess.ChangePassword("senha123");

            await _repository.AddAsync(personAccess);
        }

        [Test]
        public async Task ShouldBeUpdateNewPersonSuccessfully()
        {
            var person = _fixture.Create<Person>();
            person.SetGender(new Gender(1, "Feminino"));
            await _prepository.AddAsync(person);

            var personAccess = new PersonAccess("TESTE", new PersonAccessEvent(true, "123", DateTime.Now.AddDays(-50)));
            personAccess.SetId(person.Id);
            personAccess.Enable();
            personAccess.ChangePassword("senha123");

            await _repository.AddAsync(personAccess);
            personAccess.ChangePassword("senha12345");
            await _repository.UpdateAsync(personAccess);
        }

        [Test]
        public async Task ShouldBeGetPersonSuccessfully()
        {
            var person = _fixture.Create<Person>();
            person.SetGender(new Gender(1, "Feminino"));
            await _prepository.AddAsync(person);

            var personAccess = new PersonAccess("TESTE", new PersonAccessEvent(true, "123", DateTime.Now.AddDays(-50)));
            personAccess.SetId(person.Id);
            personAccess.Enable();
            personAccess.ChangePassword("senha123");

            await _repository.AddAsync(personAccess);

            await _repository.GetByIdAsync(person.Id);
        }
    }
}