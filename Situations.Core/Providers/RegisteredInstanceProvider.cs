namespace Situations.Core.Providers
{
    public class RegisteredInstanceProvider : InstanceProvider
    {
        private readonly Dictionary<Type, Func<object>> _registeredInstances;

        public RegisteredInstanceProvider(Dictionary<Type, Func<object>> registeredInstances)
        {
            _registeredInstances = registeredInstances;
        }

        public override object GetInstance(Type instanceType)
        {
            if (_registeredInstances.TryGetValue(instanceType, out var instanceResolver))
            {
                return instanceResolver();
            }

            return base.GetInstance(instanceType);
        }
    }
}
