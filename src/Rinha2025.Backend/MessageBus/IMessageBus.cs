namespace Rinha2025.Backend.MessageBus
{
    public interface IMessageBus
    {
        Task Publish(string message);
    }
}
