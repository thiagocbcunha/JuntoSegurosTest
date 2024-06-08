using AutoFixture;
using FluentAssertions;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Infra.Dapper.Connection;
using JuntoSeguros.Infra.Dapper.Contracts;
using JuntoSeguros.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;

namespace JuntoSeguros.Infra.Test
{
    public class PersonRepositoryTests
    {
        private Fixture _fixture = new();
        private IPersonRepository _repository;
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
            _repository = new PersonRepository(_connectionFactory);
        }

        [Test]
        public async Task ShouldBeAddNewPersonSuccessfully()
        {
            var person = _fixture.Create<Person>();
            person.SetGender(new Gender(1, "Feminino"));

            var personIdBefore = person.Id;
            await _repository.AddAsync(person);
        }

        [Test]
        public async Task ShouldBeUpdateNewPersonSuccessfully()
        {
            var person = _fixture.Create<Person>();
            person.SetGender(new Gender(1, "Feminino"));
            await _repository.AddAsync(person);

            if (person is not null)
            {
                var genderBefore = person.Gender;

                person.SetGender(new Gender(2, "Masculino"));
                await _repository.UpdateAsync(person);
            }
        }

        [Test]
        public async Task ShouldBeGetPersonSuccessfully()
        {
            var personId = Guid.Parse("BDC873D7-DC10-4CCD-937D-68BBF9BF4861");
            var person = await _repository.GetByIdAsync(personId);
        }
    }
}