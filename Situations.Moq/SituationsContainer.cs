using Situations.Core;

namespace Situations.Moq
{
    public class SituationsContainer<SituationEnum> : ISituationsContainer<SituationEnum> where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations;

        internal SituationsContainer(Registrations<SituationEnum> registrations)
        {
            _registrations = registrations;
        }

        public IConfiguredService<T, SituationEnum> GetConfiguredService<T>() where T : class
        {
            var instanceProvider = InstanceProviderFactory.GetInstanceProvider(_registrations);
            var instanceResult = instanceProvider.TryGetInstance(typeof(T));

            if (instanceResult.IsFailure)
            {
                throw instanceResult.Exception!;
            }

            var instance = (T)instanceResult.Data!;

            return new ConfiguredService<T, SituationEnum>(instance, _registrations.RegisteredSituations);
        }
    }
}
