﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyInjection
{
    abstract class ConstructionMethod
    {
        protected object[]? _parameterValues;

        public List<MethodParameter> Parameters { get; set; }

        public ConstructionMethod()
        {
            Parameters = new List<MethodParameter>();
        }

        public void SetParameterValues(object[] values)
        {
            _parameterValues = values;
        }

        public abstract object Invoke(ConstructionMethodInvokationContext context);
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
