using Situations.Core.Monads;

namespace Situations.Core.Providers
{
    public class InstanceProvider : IInstanceProvider
    {
        private readonly IConstructorProvider _constructorProvider;
        private readonly IInstanceProviderFactory _instanceProviderFactory;

        public InstanceProvider(IConstructorProvider constructorProvider, IInstanceProviderFactory parameterProvider)
        {
            _constructorProvider = constructorProvider;
            _instanceProviderFactory = parameterProvider;
        }

        public override Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                var canGetConstructor = _constructorProvider.TryGetConstructorInfo(instanceType, out var constructorInfo);

                if (canGetConstructor)
                {
                    var parameters = constructorInfo!.GetParameters();
                    List<object> paramaterInstances = new();

                    foreach (var parameter in parameters)
                    {
                        var instanceProvider = _instanceProviderFactory.GetInstanceProvider();
                        var instanceResult = instanceProvider.TryGetInstance(parameter.ParameterType);

                        if (instanceResult.IsFailure)
                        {
                            return Result<object>.Failure(instanceResult.Exception!);
                        }

                        paramaterInstances.Add(instanceResult.Data!);
                    }

                    return Result<object>.Success(constructorInfo!.Invoke(paramaterInstances.ToArray()));
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
