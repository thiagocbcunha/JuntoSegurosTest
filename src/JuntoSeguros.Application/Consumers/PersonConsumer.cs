using MassTransit;
using JuntoSeguros.Domain.Dtos;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Application.Consumers;

public class PersonConsumer(IPersonNSqlRepository _personNSqlRepository) : IConsumer<PersonDto>
{
    public async Task Consume(ConsumeContext<PersonDto> context)
    {
        _personNSqlRepository.Update(context.Message);
        await Task.CompletedTask;
    }
}