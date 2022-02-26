using System;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyInjection
{
    abstract class ConstructionMethod
    {
        protected object[] _parameterValues;

        public List<MethodParameter> Parameters { get; protected set; }

        public void SetParameterValues(object[] values)
        {
            _parameterValues = values;
        }

        public abstract object Invoke();
    }

    class MethodParameter
    {
        public MethodParameter(ParameterInfo parameterInfo)
        {
            Type = parameterInfo.ParameterType;
        }

        public Type Type { get; }
    }
}
