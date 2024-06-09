using AutoFixture;
using FluentAssertions;
using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Infra.Dapper.Connection;
using JuntoSeguros.Infra.Dapper.Contracts;
using JuntoSeguros.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;

namespace JuntoSeguros.Infra.IntegrationTest
{
    public class PersonRepositoryTests : BaseRepositoryTest
    {
        [Test]
        public async Task ShouldBeAddNewPersonSuccessfully()
        {
            var person = GetPerson(new Gender(1, "Feminino"));

            var personIdBefore = person.Id;
            await _personRepository.AddAsync(person);

            personIdBefore.Should().NotBe(person.Id);
        }

        [Test]
        public async Task ShouldBeUpdateNewPersonSuccessfully()
        {
            var person = await CreateFullPerson(new Gender(1, "Feminino"));

            var genderBefore = person.Gender;
            person.SetGender(new Gender(2, "Masculino"));
            await _personRepository.UpdateAsync(person);

            genderBefore.Should().NotBe(person.Gender);
        }

        [Test]
        public async Task ShouldBeGetPersonSuccessfully()
        {
            var person = await CreateFullPerson(new Gender(1, "Feminino"));
            var personDb = await _personRepository.GetByIdAsync(person.Id);

            personDb.Id.Should().Be(person.Id);
            personDb.Name.Should().Be(person.Name);
        }
    }
}