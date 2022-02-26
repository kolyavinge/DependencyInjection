using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    class BindingContainer
    {
        private Dictionary<Type, BindingDescription> _descriptions = new Dictionary<Type, BindingDescription>();

        public void Add(BindingDescription binding)
        {
            _descriptions.Add(binding.DependencyType, binding);
        }

        public BindingDescription GetDescription(Type dependencyType)
        {
            return _descriptions[dependencyType];
        }
    }
}
