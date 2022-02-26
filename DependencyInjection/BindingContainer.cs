using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    internal class BindingContainer
    {
        private Dictionary<Type, BindingDescription> _descriptions = new Dictionary<Type, BindingDescription>();

        public void AddDescription(BindingDescription binding)
        {
            if (binding == null)
            {
                throw new ArgumentNullException(nameof(binding));
            }
            if (_descriptions.ContainsKey(binding.DependencyType))
            {
                throw new DependencyContainerException($"Type '{binding.DependencyType}' has already been added.");
            }

            _descriptions.Add(binding.DependencyType, binding);
        }

        public BindingDescription GetDescription(Type dependencyType)
        {
            if (dependencyType == null)
            {
                throw new ArgumentNullException(nameof(dependencyType));
            }
            if (!_descriptions.ContainsKey(dependencyType))
            {
                throw new DependencyContainerException($"Type '{dependencyType}' has not been added.");
            }

            return _descriptions[dependencyType];
        }
    }
}
