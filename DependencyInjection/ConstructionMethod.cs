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
        private ParameterInfo _parameterInfo;

        public MethodParameter(ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo;
        }

        public Type Type => _parameterInfo.ParameterType;
    }
}
