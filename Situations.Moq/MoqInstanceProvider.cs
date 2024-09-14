using Moq;
using Situations.Core.Providers;

namespace Situations.Moq
{
    internal class MoqInstanceProvider : InstanceProvider
    {
        private readonly IConstructorProvider _constructorProvider;
        private readonly IInstanceProviderFactory _instanceProviderFactory;

        public MoqInstanceProvider(IConstructorProvider constructorProvider, IInstanceProviderFactory instanceProviderFactory)
        {
            _constructorProvider = constructorProvider;
            _instanceProviderFactory = instanceProviderFactory;
        }

        public override object GetInstance(Type instanceType)
        {
            if (_constructorProvider.TryGetConstructorInfo(instanceType, out var constructorInfo))
            {
                var parameters = constructorInfo!.GetParameters();
                List<object> parameterInstances = new();

                foreach (var parameter in parameters)
                {
                    var instanceProvider = _instanceProviderFactory.GetInstanceProvider();
                    parameterInstances.Add(instanceProvider.GetInstance(parameter.ParameterType));
                }

                var mockType = typeof(Mock<>).MakeGenericType(instanceType);
                dynamic mockObject = Activator.CreateInstance(mockType, parameterInstances)!;

                return mockObject.Object;
            }
            else if (instanceType.IsInterface)
            {
                var mockType = typeof(Mock<>).MakeGenericType(instanceType);
                dynamic mockObject = Activator.CreateInstance(mockType)!;

                return mockObject.Object;
            }

            return base.GetInstance(instanceType);
        }
    }
}
