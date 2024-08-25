using Situations.Core;
using Situations.Moq;

namespace Situations
{
    public class SituationsContainer<SituationEnum> : ISituationsContainer<SituationEnum> where SituationEnum : Enum
    {
        private readonly IEnumerable<IRegisteredSituation<SituationEnum>> _situationRegistrations;
        private readonly IEnumerable<IRegisteredInstance> _registeredInstances;

        internal SituationsContainer(IEnumerable<IRegisteredSituation<SituationEnum>> situationConditions, IEnumerable<IRegisteredInstance> registeredInstances)
        {
            _situationRegistrations = situationConditions;
            _registeredInstances = registeredInstances;
        }

        public IConfiguredService<T, SituationEnum> GetConfiguredService<T>() where T : class
        {
            var instanceProvider = new InstanceProvider<SituationEnum>(new MoqInstanceProvider<SituationEnum>(_registeredInstances), _situationRegistrations);
            var instance = (T)instanceProvider.GetInstance(typeof(T));

            return new ConfiguredService<T, SituationEnum>(instance, _situationRegistrations);
        }
    }
}
