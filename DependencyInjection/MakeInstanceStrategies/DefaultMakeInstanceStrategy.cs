namespace DependencyInjection.MakeInstanceStrategies
{
    internal class DefaultMakeInstanceStrategy : MakeInstanceStrategy
    {
        public static readonly DefaultMakeInstanceStrategy Instance = new DefaultMakeInstanceStrategy();

        private DefaultMakeInstanceStrategy() { }

        public override object GetInstance()
        {
            return null;
        }
    }
}
