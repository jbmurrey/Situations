using Situations.Core;
using Situations.Core.Providers;

namespace Situations.Moq
{
    public class SituationsContainer<SituationEnum> where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations;
        private readonly IInstanceProviderFactory _instanceProviderFactory;

        public SituationsContainer(IInstanceProviderFactory instanceProviderFactory, Registrations<SituationEnum> registrations)
        {
            _instanceProviderFactory = instanceProviderFactory;
            _registrations = registrations;
        }

        public IConfiguredService<T, SituationEnum> GetConfiguredService<T>() where T : class
        {
            var instanceResult = _instanceProviderFactory.GetInstanceProvider().TryGetInstance(typeof(T));

            if (instanceResult.IsFailure)
            {
                throw instanceResult.Exception!;
            }

            var instance = (T)instanceResult.Data!;

            return new ConfiguredService<T, SituationEnum>(instance, _registrations.RegisteredSituations);
        }
    }
}
