using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroFx.Extensibility
{
    public class ExtensibleModules
    {
        private readonly List<Func<IExtensibleModule>> moduleFuncs;
        private List<IExtensibleModule> module;

        public ExtensibleModules()
        {
            moduleFuncs = new List<Func<IExtensibleModule>>();
        }
        private void Initialise()
        {
            module = moduleFuncs.Select(y => y()).ToList();
            var current = module[0];
            foreach (var next in module)
            {
                if (current == next)
                    continue;
                var next1 = next;
                current.Init(c => next1.Register(c));
                current = next;
            }
        }

        public void Register(IRegisterContext context)
        {
            Initialise();
            var result = module[0].Register(context);
        }

        public void Add(IExtensibleModule mod)
        {
            moduleFuncs.Add(()=> mod);
        }
    }
}