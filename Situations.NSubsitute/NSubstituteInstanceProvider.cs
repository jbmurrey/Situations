using NSubstitute;
using Situations.Core.Providers;

namespace Situations.NSubsitute
{
    internal class NSubstituteInstanceProvider : InstanceProvider
    {
        public override object GetInstance(Type instanceType)
        {
            if (instanceType.IsAbstract || instanceType.IsInterface)
            {
                return Substitute.For(new[] { instanceType }, null);

            }

            return base.GetInstance(instanceType);
        }
    }
}
