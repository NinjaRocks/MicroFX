namespace MicroFx
{
    public interface IServiceClient
    {
        TResponse Get<TResponse>(string endpoint);
        TResponse Post<TRequest, TResponse>(string endpoint, TRequest request);
        TResponse Put<TRequest, TResponse>(string endpoint, TRequest request);
        TResponse Delete<TResponse>(string endpoint);
    }
}