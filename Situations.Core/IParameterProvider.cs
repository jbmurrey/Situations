using System.Reflection;
namespace Situations.Core
{
    public interface IParameterProvider
    {
        IEnumerable<object> GetParameters(ConstructorInfo constructorInfo);
    }
}
