using Moq;
using Situations.Core.InstanceProviders;
using Situations.Core.Monads;

namespace Situations.Moq
{
    internal class MoqInstanceHandler : IInstanceProvider
    {
        private readonly Dictionary<Type, Func<object>> _registeredInstances;

        public MoqInstanceHandler(Dictionary<Type, Func<object>> registeredInstances)
        {
            _registeredInstances = registeredInstances;
        }

        public override Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                if (instanceType.IsAbstract || instanceType.IsInterface)
                {
                    var mockType = typeof(Mock<>).MakeGenericType(instanceType);
                    dynamic mockObject = Activator.CreateInstance(mockType)!;

                    return Result<object>.Success(mockObject.Object);
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
