using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Dtos;
using MassTransit;

namespace JuntoSeguros.Application.Consumers;

public class PersonAccessConsumer(IPersonAccessNSqlRepository _personAccessNSqlRepository) : IConsumer<PersonAccessDto>
{
    public async Task Consume(ConsumeContext<PersonAccessDto> context)
    {
        _personAccessNSqlRepository.Insert(context.Message);
        await Task.CompletedTask;
    }
}
