using MassTransit;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Consumers;

public class PersonAccessConsumer(IPersonAccessNSqlRepository _personAccessNSqlRepository) : IConsumer<PersonAccessDto>
{
    public async Task Consume(ConsumeContext<PersonAccessDto> context)
    {
        _personAccessNSqlRepository.Update(context.Message);
        await Task.CompletedTask;
    }
}