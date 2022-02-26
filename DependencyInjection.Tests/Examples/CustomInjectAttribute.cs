using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomInjectAttribute : InjectAttribute
    {
    }
}
