namespace Situations.Core
{
    public interface IRegisteredInstance
    {
        Func<object> InstanceResolver { get; }
        Type RegisteredType { get; }
    }
}
