using System.Reflection;

namespace Situations.Core.Providers
{
    public class RegisteredConstructorProvider : IConstructorProvider
    {
        private readonly Dictionary<Type, Func<Type, ConstructorInfo>> _constructorProviderResolvers;

        public RegisteredConstructorProvider(Dictionary<Type, Func<Type, ConstructorInfo>> constructorProviderResolvers)
        {
            _constructorProviderResolvers = constructorProviderResolvers;
        }

        public bool TryGetConstructorInfo(Type type, out ConstructorInfo? constructorInfo)
        {
            constructorInfo = null;
            var canGetConstructor = _constructorProviderResolvers.TryGetValue(type, out var constructorInfoResolver);

            if (!canGetConstructor) return false;

            constructorInfo = constructorInfoResolver!(type);
            return true;
        }
    }
}
