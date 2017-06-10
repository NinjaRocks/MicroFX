using Autofac;

namespace MicroFx.Extensibility
{
    public interface IRegisterContext
    {
       ContainerBuilder builder { get; }
    }
}