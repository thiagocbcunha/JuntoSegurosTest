using JuntoSeguros.Domain.Contracts;
using JuntoSeguros.Domain.Dtos;
using MassTransit;

namespace JuntoSeguros.Application.Consumers;

public class PersonConsumer(IPersonNSqlRepository _personNSqlRepository) : IConsumer<PersonDto>
{
    public async Task Consume(ConsumeContext<PersonDto> context)
    {
        _personNSqlRepository.Insert(context.Message);
        await Task.CompletedTask;
    }
}
