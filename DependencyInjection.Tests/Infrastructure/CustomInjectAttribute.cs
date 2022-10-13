using System;

namespace DependencyInjection.Tests.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomInjectAttribute : InjectAttribute
    {
    }
}
