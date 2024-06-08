using AutoFixture;
using FluentAssertions;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Domain.Entities.PersonEntity;

namespace JuntoSeguros.Domain.Test
{
    public class PersonTests
    {
        private Fixture _fixture = new();

        [Test]
        public void ShoudChangePasswordSuccessfully()
        {
            var newGender = _fixture.Create<Gender>();
            var person = _fixture.Create<Person>();
            
            person.SetGender(newGender);

            person.Gender.Should().Be(newGender);
        }
    }
}