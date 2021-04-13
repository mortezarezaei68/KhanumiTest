using Framework.Commands.CommandHandlers;

namespace Framework.Commands.CommandHandlerWithMassTransit
{
    public interface IMassTransitMessageHandler<in TCommand, TResponse>
        where TCommand : IMessage<TResponse> where TResponse : ResponseCommand
    {
        
    }

    public interface IMessage<TResponse>
    {
    }
}