using System;
using DependencyInjection.Common;
using DependencyInjection.Resolving;
using Moq;

namespace DependencyInjection.Tests.Infrastructure
{
    internal class BaseResolverTest<TResolver> : IResolvingProvider where TResolver : Resolver, new()
    {
        protected BindingContainer _bindingContainer;
        protected Mock<IInvokationContext> _invokationContextMock;
        protected TResolver _resolver;

        public void Init()
        {
            _bindingContainer = new BindingContainer();
            _invokationContextMock = new Mock<IInvokationContext>();
            _invokationContextMock.Setup(x => x.ResolvingProvider).Returns(this);
            _resolver = new TResolver();
            _resolver.Init(_bindingContainer, _invokationContextMock.Object);
        }

        public T Resolve<T>()
        {
            return (T)_resolver.Resolve(typeof(T));
        }

        public object Resolve(Type dependencyType)
        {
            return _resolver.Resolve(dependencyType);
        }

        protected BindingDescription Bind<TDependencyType, TImplementationType>()
        {
            var desc = new BindingDescription(typeof(TDependencyType), typeof(TImplementationType));
            _bindingContainer.AddDescription(desc);

            return desc;
        }

        protected BindingDescription Bind<TDependencyType>()
        {
            var desc = new BindingDescription(typeof(TDependencyType), typeof(TDependencyType));
            _bindingContainer.AddDescription(desc);

            return desc;
        }
    }
}
