using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class DependencyContainerException : Exception
    {
        public DependencyContainerException(string message) : base(message)
        {
        }
    }
}
