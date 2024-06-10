using MassTransit;
using JuntoSeguros.Domain.Contracts;

namespace JuntoSeguros.Infra.RabbitMQ;

public class MessageSender(IBus _bus, Enterprise.Library.Contracts.IActivityFactory _activityFactory) : IMessagingSender
{
    public async Task Send<TMessage>(TMessage message) where TMessage : class
    {
        _activityFactory.Start("MessageSender - New Event");
        var sendEndpoint = await _bus.GetSendEndpoint(new Uri($"queue:JuntoSeguros.{typeof(TMessage).Name}.Event"));
        await sendEndpoint.Send(message);
    }
}