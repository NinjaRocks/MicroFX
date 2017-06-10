using System;

namespace MicroFx
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string msg):base(msg)
        {
            
        }
    }
}