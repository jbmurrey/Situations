using Moq;
using Situations.Core.Monads;
using Situations.Core.Providers;

namespace Situations.Moq
{
    internal class MoqInstanceProvider : IInstanceProvider
    {
        public override Result<object> TryGetInstance(Type instanceType)
        {
            try
            {
                if (instanceType.IsAbstract || instanceType.IsInterface)
                {
                    var mockType = typeof(Mock<>).MakeGenericType(instanceType);
                    dynamic mockObject = Activator.CreateInstance(mockType)!;

                    return Result<object>.Success(mockObject.Object);
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
