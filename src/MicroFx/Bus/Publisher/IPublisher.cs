namespace MicroFx.Bus.Publisher
{
    public interface IPublisher: IRegisterEvents, IRegisterCommands
    {
        bool Publish<T>(T message) where T : IMessage;
    }
}