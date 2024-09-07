using Situations.Core;
using Situations.Moq;

namespace Situations
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
            var mockInstanceProvider = new MoqInstanceProvider(_registrations.RegisteredInstances);
            var constructorProvider = new DefaultConstructorProvider(new MoqConstructorProvider(_registrations.RegisteredConstructors));
            var instanceProvider = new InstanceProvider(mockInstanceProvider, constructorProvider, new DefaultParameterProvider(mockInstanceProvider, _registrations.RegisteredInstances, constructorProvider));
            var tryGetInstanceResult = instanceProvider.TryGetInstance(typeof(T));

            if (tryGetInstanceResult.IsFailure)
            {
                throw tryGetInstanceResult.Exception!;
            }

            var instance = (T)tryGetInstanceResult.Data!;

            return new ConfiguredService<T, SituationEnum>(instance, _registrations.RegisteredSituations);
        }
    }
}
