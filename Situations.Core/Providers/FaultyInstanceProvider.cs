using Situations.Core.Exceptions;
using Situations.Core.Monads;

namespace Situations.Core.InstanceProviders
{
    public class FaultyInstanceProvider : IInstanceProvider
    {
        public override Result<object> TryGetInstance(Type instanceType)
        {
            return Result<object>.Failure(new UnregisteredInstanceException($"Unable to create instance for type: {instanceType}  "));
        }
    }
}
