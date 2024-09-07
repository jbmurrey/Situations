using Situations.Core;
using Situations.Moq;
using System.Reflection;

namespace Situations
{
    public class SituationsBuilder<SituationEnum> where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations = new();

        public RegisteredSituation<SituationEnum, TService> RegisterSituation<TService>(SituationEnum situation)
            where TService : class
        {
            var sitatuion = new RegisteredSituation<SituationEnum, TService>(situation, MockSingletonFactory<TService>.Instance);
            _registrations.RegisteredSituations[situation] = sitatuion;

            return sitatuion;
        }

        public void RegisterInstance<TImplementation, TService>(Func<TService> instanceResolver)
        {
            _registrations.RegisteredInstances[typeof(TService)] = () => instanceResolver()!;
        }

        public void RegisterConstructor<TService>(Func<Type, ConstructorInfo> constructorResolver)
        {
            _registrations.RegisteredConstructors[typeof(TService)] = constructorResolver;
        }

        public ISituationsContainer<SituationEnum> Build()
        {
            return new SituationsContainer<SituationEnum>(_registrations);
        }
    }
}