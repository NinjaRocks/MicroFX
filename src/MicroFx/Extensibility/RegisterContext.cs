using Autofac;

namespace MicroFx.Extensibility
{
    public class RegisterContext: IRegisterContext
    {
        public RegisterContext(ContainerBuilder builder)
        {
            this.builder = builder;
        }
        public ContainerBuilder builder { get; }
    }
}