using System;

namespace MicroFx
{
    public class BusinessException : Exception
    {
        public BusinessException(string msg)
            : base(msg)
        {

        }
    }
}