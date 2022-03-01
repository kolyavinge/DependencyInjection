using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public class TestInjectModule : InjectModule
    {
        public override void Init(IBindingProvider bindingProvider)
        {
            bindingProvider.Bind<IDependency, DependencyImplementation>();
            bindingProvider.Bind<ISomeClass, SomeClassImplementation>();
        }
    }
}
