using System.Reflection;

namespace Situations.Core.Providers
{
    public class ParameterProvider : IParameterProvider
    {

        private readonly Dictionary<Type, Func<object>> _instanceResolvers;
        private readonly IConstructorProvider _constructorProvider;
        private IInstanceProviderFactory _instanceProviderFactory;

        public ParameterProvider(Dictionary<Type, Func<object>> instanceResolvers, IConstructorProvider constructorProvider, IInstanceProviderFactory instanceProviderFactory)
        {
            _instanceResolvers = instanceResolvers;
            _constructorProvider = constructorProvider;
            _instanceProviderFactory = instanceProviderFactory;
        }

        public IEnumerable<object> GetParameters(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
            {
                throw new ArgumentNullException(nameof(constructorInfo));
            }

            return GetParameters(constructorInfo.GetParameters());
        }

        private IEnumerable<object> GetParameters(ParameterInfo[] constructorParameters)
        {
            foreach (var constructorParameter in constructorParameters)
            {
                if (_instanceResolvers.TryGetValue(constructorParameter.ParameterType, out var instanceResolver))
                {
                    yield return instanceResolver();
                }
                else
                {
                    var instanceProvider = _instanceProviderFactory!.GetInstanceProvider();
                    var instanceResult = instanceProvider.TryGetInstance(constructorParameter.ParameterType);


                    if (instanceResult.IsSuccess)
                    {
                        yield return instanceResult.Data!;
                    }
                    else
                    {
                        throw instanceResult.Exception!;
                    }
                }
            }
        }
    }
}
