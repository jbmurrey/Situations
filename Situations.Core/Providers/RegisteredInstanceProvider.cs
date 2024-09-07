using Situations.Core.Monads;

namespace Situations.Core.Providers
{
    public class RegisteredInstanceProvider : IInstanceProvider
    {
        private readonly Dictionary<Type, Func<object>> _registeredInstances;

        public RegisteredInstanceProvider(Dictionary<Type, Func<object>> registeredInstances)
        {
            _registeredInstances = registeredInstances;
        }

        public override Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                if (_registeredInstances.TryGetValue(instanceType, out var instanceResolver))
                {
                    return Result<object>.Success(instanceResolver());
                }

                return _instanceProvider.TryGetInstance(instanceType);
            }
            catch (Exception ex)
            {
                return Result<object>.Failure(ex);
            }
        }
    }
}
