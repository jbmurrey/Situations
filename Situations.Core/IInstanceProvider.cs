namespace Situations.Core
{
    public interface IInstanceProvider
    {
        Result<object> TryGetInstance(Type instanceType);
    }
}
