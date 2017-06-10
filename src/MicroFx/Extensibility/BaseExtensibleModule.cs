using System;

namespace MicroFx.Extensibility
{
    public abstract class BaseExtensibleModule: IExtensibleModule
    {
        private Func<IRegisterContext, bool> next;

        public void Init(Func<IRegisterContext, bool> nextMiddleware)
        {
            next = nextMiddleware;
        }

        public virtual bool Next(IRegisterContext context)
        {
            return next == null || next(context);
        }

        public abstract bool Register(IRegisterContext context);
    }
}
