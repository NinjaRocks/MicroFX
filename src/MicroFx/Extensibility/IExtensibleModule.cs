using System;

namespace MicroFx.Extensibility
{
    public interface IExtensibleModule
    {
        void Init(Func<IRegisterContext, bool> nextMiddleware);
        bool Register(IRegisterContext context);
    }
}