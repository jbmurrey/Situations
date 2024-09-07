using NSubstitute;
using Situations.Core.Monads;
using Situations.Core.Providers;

namespace Situations.NSubsitute
{
    internal class NSubstituteInstanceProvider : IInstanceProvider
    {
        public override Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                if (instanceType.IsAbstract || instanceType.IsInterface)
                {
                    var instance = Substitute.For(new[] { instanceType }, null);
                    return Result<object>.Success(instance);
                }

                return _instanceProvider.TryGetInstance(instanceType);

            }
            catch (Exception ex)
            {
                return Result<object>.Failure(ex);
            }
        }
    }
}
