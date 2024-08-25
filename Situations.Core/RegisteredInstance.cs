namespace Situations.Core
{
    public class RegisteredInstance : IRegisteredInstance
    {
        public RegisteredInstance(Func<object> instanceResolver, Type registeredType)
        {
            InstanceResolver = instanceResolver;
            RegisteredType = registeredType;
        }

        public Func<object> InstanceResolver { get; }

        public Type RegisteredType { get; }
    }
}
