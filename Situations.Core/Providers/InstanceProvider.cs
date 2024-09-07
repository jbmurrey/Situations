using Situations.Core.Monads;

namespace Situations.Core.Providers
{
    public class InstanceProvider : IInstanceProvider
    {
        private readonly IConstructorProvider _constructorProvider;
        private readonly IParameterProvider _parameterProvider;

        public InstanceProvider(IConstructorProvider constructorProvider, IParameterProvider parameterProvider)
        {
            _constructorProvider = constructorProvider;
            _parameterProvider = parameterProvider;
        }

        public override Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                var canGetConstructor = _constructorProvider.TryGetConstructorInfo(instanceType, out var constructorInfo);

                if (canGetConstructor)
                {
                    var parameters = constructorInfo!.GetParameters();
                    var parameterInstances = _parameterProvider.GetParameters(constructorInfo).ToArray();

                    return Result<object>.Success(constructorInfo!.Invoke(parameterInstances));
                }

                return _instanceProvider.TryGetInstance(instanceType);
            }
            catch (Exception ex)
            {
                {
                    return Result<object>.Failure(ex);
                }
            }
        }
    }
}
