using Situations.Core.Exceptions;

namespace Situations.Core
{
    public class DefaultInstanceProvider : IInstanceProvider
    {
        private readonly IInstanceProvider _innerHandler;
        private readonly IConstructorProvider _constructorProvider;
        private readonly IParameterProvider _parameterProvider;

        public DefaultInstanceProvider(IInstanceProvider innerHandler, IConstructorProvider constructorProvider, IParameterProvider parameterProvider)
        {
            _innerHandler = innerHandler;
            _constructorProvider = constructorProvider;
            _parameterProvider = parameterProvider;
        }

        public Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                var innerHandlerInstanceResult = _innerHandler.TryGetInstance(instanceType);

                if (innerHandlerInstanceResult.IsSuccess)
                {
                    return innerHandlerInstanceResult;
                }

                var canGetConstructor = _constructorProvider.TryGetConstructorInfo(instanceType, out var constructorInfo);

                if (!canGetConstructor)
                {
                    return Result<object>.Failure(new NoSuitableConstructorException($"No suitable constructor found for type {instanceType}"));
                }

                var parameters = constructorInfo!.GetParameters();
                var parameterInstances = _parameterProvider.GetParameters(constructorInfo).ToArray();

                return Result<object>.Success(constructorInfo!.Invoke(parameterInstances));
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
