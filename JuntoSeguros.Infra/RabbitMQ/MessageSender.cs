using JuntoSeguros.Domain.Contracts;
using MassTransit;

namespace JuntoSeguros.Infra.RabbitMQ;

public class MessageSender(IBus _bus) : IMessagingSender
{
    public async Task Send<TMessage>(TMessage message) where TMessage : class
    {
        var sendEndpoint = await _bus.GetSendEndpoint(new Uri($"queue:JuntoSeguros.{typeof(TMessage).Name}.Event"));
        await sendEndpoint.Send(message);
    }
}