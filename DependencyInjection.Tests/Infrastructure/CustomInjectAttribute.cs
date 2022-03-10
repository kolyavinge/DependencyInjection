using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomInjectAttribute : InjectAttribute
    {
    }
}
