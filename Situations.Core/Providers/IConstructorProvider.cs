using System.Reflection;

namespace Situations.Core.Providers
{
    public interface IConstructorProvider
    {
        bool TryGetConstructorInfo(Type type, out ConstructorInfo? constructorInfo);
    }
}
