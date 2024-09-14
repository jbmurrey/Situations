using Situations.Core.Providers;

namespace Situations.Core
{
    public class SituationsContainer<SituationEnum> where SituationEnum : Enum
    {
        private readonly Dictionary<SituationEnum, IRegisteredSituation<SituationEnum>> _registeredSituations;
        private readonly IInstanceProviderFactory _instanceProviderFactory;

        public SituationsContainer(IInstanceProviderFactory instanceProviderFactory, Dictionary<SituationEnum, IRegisteredSituation<SituationEnum>> registeredSituations)
        {
            _instanceProviderFactory = instanceProviderFactory;
            _registeredSituations = registeredSituations;
        }

        public IConfiguredService<TService, SituationEnum> GetConfiguredService<TService>() where TService : class
        {
            var instanceProvider = _instanceProviderFactory.GetInstanceProvider();
            var instance = (TService)instanceProvider.GetInstance(typeof(TService));

            return new ConfiguredService<TService, SituationEnum>(instance, _registeredSituations);
        }
    }
}
