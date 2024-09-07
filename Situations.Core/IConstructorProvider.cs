using System.Reflection;

namespace Situations.Core
{
    public interface IConstructorProvider
    {
        bool TryGetConstructorInfo(Type type, out ConstructorInfo? constructorInfo);
    }
}
