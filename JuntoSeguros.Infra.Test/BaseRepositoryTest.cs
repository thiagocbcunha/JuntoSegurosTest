using AutoFixture;
using Microsoft.Extensions.Configuration;
using JuntoSeguros.Infra.Dapper.Connection;
using JuntoSeguros.Infra.Dapper.Repositories;
using JuntoSeguros.Domain.Entities.PersonEntity;
using JuntoSeguros.Domain.Entities.PersonAccessEntity;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Infra.IntegrationTest;

public abstract class BaseRepositoryTest
{
    protected Fixture _fixture = new();
    protected IPersonRepository _personRepository;
    protected IPersonAccessRepository _personAccessRepository;

    public BaseRepositoryTest()
    {
        var connectionString = new Dictionary<string, string>
        {
                {"ConnectionStrings:JuntoSegurosOnboarding", "Server=127.0.0.1,1433; Database=JuntoSegurosOnboarding; User Id=sa;Password=SqlServer2022!;"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData: connectionString)
        .Build();

        var connectionFactory = new DapperConnectionFactory(configuration);
        _personRepository = new PersonRepository(connectionFactory);
        _personAccessRepository = new PersonAccessRepository(connectionFactory);
    }

    public Person GetPerson(Gender gender)
    {
        var person = _fixture.Create<Person>();
        person.SetGender(gender);

        return person;
    }
    public PersonAccess GetPersonAccess(Gender gender)
    {
        var person = GetPerson(gender);
        return GetPersonAccess(person.Id);
    }

    public PersonAccess GetPersonAccess(Guid personId)
    {
        var Email = _fixture.Create<string>();
        var password = _fixture.Create<string>();
        var personAccess = new PersonAccess(Email, new PersonAccessEvent(true, password, DateTime.Now.AddDays(-50)));
        
        personAccess.Enable();
        personAccess.SetId(personId);

        return personAccess;
    }

    public async Task<PersonAccess> CreateFullPersonAccess()
    {
        var person = await CreateFullPerson();

        var personAccess = GetPersonAccess(person.Id);
        personAccess.ChangePassword(_fixture.Create<string>());
        await _personAccessRepository.AddAsync(personAccess);

        return personAccess;
    }

    public async Task<Person> CreateFullPerson(Gender gender)
    {
        var person = GetPerson(gender);
        await _personRepository.AddAsync(person);

        return person;
    }

    public async Task<Person> CreateFullPerson()
    {
        return await CreateFullPerson(new Gender(1, "Feminino"));
    }
}