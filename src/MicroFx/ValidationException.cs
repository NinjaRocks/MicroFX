using System;

namespace MicroFx
{
    public class ValidationException : Exception
    {
        public ValidationException(string msg)
            : base(msg)
        {

        }
    }
}