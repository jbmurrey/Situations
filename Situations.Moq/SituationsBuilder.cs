using Moq;
using Situations.Core;
using Situations.Moq;

namespace Situations
{
    public class SituationsBuilder<SituationEnum> where SituationEnum : Enum
    {
        private readonly List<IRegisteredSituation<SituationEnum>> _registeredSituations = new();
        private readonly List<IRegisteredInstance> _registeredInstanceProviders = new();
        public RegisteredSituation<SituationEnum, TService> RegisterSituation<TService>(SituationEnum situation)
            where TService : class
        {
            RegisteredSituation<SituationEnum, TService> situationRegistration;

            var existingRegisteredServiceTypes = _registeredSituations.Where(x => x.RegistrationType == typeof(TService));

            if (existingRegisteredServiceTypes.Any() && existingRegisteredServiceTypes.First() is RegisteredSituation<SituationEnum, TService> existingSituationRegistration)
            {
                situationRegistration = new RegisteredSituation<SituationEnum, TService>(situation, existingSituationRegistration.Mock);
            }
            else
            {
                situationRegistration = new RegisteredSituation<SituationEnum, TService>(situation, new Mock<TService>());
            }

            _registeredSituations.Add(situationRegistration);
            return situationRegistration;
        }

        public void RegisterInstance<TImplementation, TService>(Func<TService> instanceResolver) where TImplementation : TService
        {
            _registeredInstanceProviders.Add(new RegisteredInstance(() => instanceResolver()!, typeof(TService)));
        }

        public ISituationsContainer<SituationEnum> Build()
        {
            return new SituationsContainer<SituationEnum>(_registeredSituations, _registeredInstanceProviders);
        }
    }
}