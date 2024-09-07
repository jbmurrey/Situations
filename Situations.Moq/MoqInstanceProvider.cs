using Moq;
using Situations.Core;
using Situations.Core.Exceptions;

namespace Situations.Moq
{
    internal class MoqInstanceProvider : IInstanceProvider
    {
        private readonly Dictionary<Type, Func<object>> _registeredInstances;

        public MoqInstanceProvider(Dictionary<Type, Func<object>> registeredInstances)
        {
            _registeredInstances = registeredInstances;
        }

        public Result<object> TryGetInstance(Type instanceType)
        {
            if (_registeredInstances.TryGetValue(instanceType, out var instanceResolver))
            {
                return Result<object>.Success(instanceResolver());
            }

            if (instanceType.IsAbstract || instanceType.IsInterface)
            {
                var mockType = typeof(Mock<>).MakeGenericType(instanceType);
                dynamic mockObject = Activator.CreateInstance(mockType)!;

                return Result<object>.Success(mockObject.Object);
            }

            return Result<object>.Failure(new UnregisteredInstanceException($"No registered Instance found for type {instanceType}  "));
        }
    }
}
