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

        public IConfiguredService<T, SituationEnum> GetConfiguredService<T>() where T : class
        {
            var instanceResult = _instanceProviderFactory.GetInstanceProvider().TryGetInstance(typeof(T));

            if (instanceResult.IsFailure)
            {
                throw instanceResult.Exception!;
            }

            var instance = (T)instanceResult.Data!;

            return new ConfiguredService<T, SituationEnum>(instance, _registeredSituations);
        }
    }
}
