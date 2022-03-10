using System;
using System.Collections.Generic;
using DependencyInjection.Common;

namespace DependencyInjection.Resolving
{
    internal abstract class Resolver
    {
        protected BindingContainer? _bindingContainer;
        protected IInvokationContext? _invokationContext;

        public List<object> Instances { get; }

        public Resolver()
        {
            Instances = new List<object>();
        }

        public void Init(BindingContainer bindingContainer, IInvokationContext invokationContext)
        {
            _bindingContainer = bindingContainer;
            _invokationContext = invokationContext;
        }

        public abstract object Resolve(Type dependencyType);
    }
}
