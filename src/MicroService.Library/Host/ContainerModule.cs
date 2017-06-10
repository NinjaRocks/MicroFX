using System;
using Autofac;

namespace MicroService.Library.Host
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           Console.WriteLine("custom autofac module ...has run");
        }
    }
}
