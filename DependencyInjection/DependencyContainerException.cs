using System;

namespace DependencyInjection;

public class DependencyContainerException : Exception
{
    public DependencyContainerException(string message) : base(message)
    {
    }
}
