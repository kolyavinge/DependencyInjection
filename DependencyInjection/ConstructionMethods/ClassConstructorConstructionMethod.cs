using System.Linq;
using System.Reflection;

namespace DependencyInjection.ConstructionMethods
{
    internal class ClassConstructorConstructionMethod : ConstructionMethod
    {
        private readonly ConstructorInfo _constructorInfo;

        public ClassConstructorConstructionMethod(ConstructorInfo constructorInfo)
        {
            _constructorInfo = constructorInfo;
            Parameters = _constructorInfo.GetParameters().Select(p => new MethodParameter(p)).ToList();
        }

        public override object Invoke(ConstructionMethodInvokationContext context)
        {
            return _constructorInfo.Invoke(_parameterValues!);
        }
    }
}
