using System.Reflection;

namespace Situations.Core
{
    public class DefaultParameterProvider : IParameterProvider
    {

        private readonly Dictionary<Type, Func<object>> _instanceResolvers;
        private readonly IConstructorProvider _constructorProvider;
        private readonly IInstanceProvider _instanceProvider;

        public DefaultParameterProvider(Dictionary<Type, Func<object>> instanceResolvers, IConstructorProvider constructorProvider, IInstanceProvider instanceProvider)
        {
            _instanceResolvers = instanceResolvers;
            _constructorProvider = constructorProvider;
            _instanceProvider = new DefaultInstanceProvider(instanceProvider, constructorProvider, this);
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
                    var tryGetInstanceResult = _instanceProvider.TryGetInstance(constructorParameter.ParameterType);

                    if (tryGetInstanceResult.IsSuccess)
                    {
                        yield return tryGetInstanceResult.Data!;
                    }
                    else
                    {
                        throw tryGetInstanceResult.Exception!;
                    }
                }
            }
        }
    }
}
