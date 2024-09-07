using System.Reflection;
namespace Situations.Core.Providers
{
    public interface IParameterProvider
    {
        IEnumerable<object> GetParameters(ConstructorInfo constructorInfo);
    }
}
