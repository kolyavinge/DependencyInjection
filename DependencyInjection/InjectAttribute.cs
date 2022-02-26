using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }
}
