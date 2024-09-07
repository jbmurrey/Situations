using Situations.Core.InstanceProviders;

namespace Situations.Core.Providers
{
    public interface IInstanceProviderFactory
    {
        IInstanceProvider GetInstanceProvider();
    }
}