namespace Situations.Core.Providers
{
    public class DefaultInstanceProvider : InstanceProvider
    {
        private readonly IConstructorProvider _constructorProvider;
        private readonly IInstanceProviderFactory _instanceProviderFactory;

        public DefaultInstanceProvider(IConstructorProvider constructorProvider, IInstanceProviderFactory parameterProvider)
        {
            _constructorProvider = constructorProvider;
            _instanceProviderFactory = parameterProvider;
        }

        public override object GetInstance(Type instanceType)
        {
            var canGetConstructor = _constructorProvider.TryGetConstructorInfo(instanceType, out var constructorInfo);

            if (canGetConstructor)
            {
                var parameters = constructorInfo!.GetParameters();
                List<object> paramaterInstances = new();

                foreach (var parameter in parameters)
                {
                    var instanceProvider = _instanceProviderFactory.GetInstanceProvider();
                    var instanceResult = instanceProvider.GetInstance(parameter.ParameterType);

                    paramaterInstances.Add(instanceResult);
                }

                return constructorInfo!.Invoke(paramaterInstances.ToArray());
            }

            return base.GetInstance(instanceType);
        }
    }
}
