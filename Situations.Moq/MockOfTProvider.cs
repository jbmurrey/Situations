using Moq;
using Situations.Core.Exceptions;
using Situations.Core.Providers;

namespace Situations.Moq
{
    public class MockOfTProvider<SituationEnum>
        where SituationEnum : Enum
    {
        private readonly IConstructorProvider _constructorProvider;
        private readonly IInstanceProviderFactory _instanceProviderFactory;

        public MockOfTProvider(IConstructorProvider constructorProvider, IInstanceProviderFactory instanceProviderFactory)
        {
            _constructorProvider = constructorProvider;
            _instanceProviderFactory = instanceProviderFactory;
        }

        public Mock<T> GetMock<T>() where T : class
        {
            if (_constructorProvider.TryGetConstructorInfo(typeof(T), out var constructorInfo))
            {
                var parameterTypes = constructorInfo!.GetParameters();
                var parameters = new List<object>();

                foreach (var parameterType in parameterTypes)
                {
                    var instanceProvider = _instanceProviderFactory.GetInstanceProvider();
                    parameters.Add(instanceProvider.GetInstance(typeof(T)));
                }

                return new Mock<T>(parameters.ToArray());
            }
            else if (typeof(T).IsInterface)
            {
                return new Mock<T>();
            }

            throw new InstanceCreationException(typeof(T));
        }
    }
}
