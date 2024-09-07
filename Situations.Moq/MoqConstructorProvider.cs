using Situations.Core.Providers;
using System.Reflection;

namespace Situations.Moq
{
    internal class MoqConstructorProvider : IConstructorProvider
    {
        private readonly Dictionary<Type, Func<Type, ConstructorInfo>> _constructorProviderResolvers;

        public MoqConstructorProvider(Dictionary<Type, Func<Type, ConstructorInfo>> constructorProviderResolvers)
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
