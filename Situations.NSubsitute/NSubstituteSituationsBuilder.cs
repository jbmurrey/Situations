using Situations.Core;
using System.Reflection;

namespace Situations.NSubsitute
{
    public class NSubstituteSituationsBuilder<SituationEnum> where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations = new();

        public RegisteredSituation<SituationEnum, TService> RegisterSituation<TService>(SituationEnum situation)
            where TService : class
        {
            var instance = NSubstituteSingletonFactory<TService>.Instance;
            var sitatuion = new RegisteredSituation<SituationEnum, TService>(situation, instance);

            _registrations.RegisteredInstanceResolvers[typeof(TService)] = () => instance;
            _registrations.RegisteredSituations[situation] = sitatuion;

            return sitatuion;
        }

        public void RegisterInstance<TImplementation, TService>(Func<TService> instanceResolver)
        {
            _registrations.RegisteredInstanceResolvers[typeof(TService)] = () => instanceResolver()!;
        }

        public void RegisterConstructor<TService>(Func<Type, ConstructorInfo> constructorResolver)
        {
            _registrations.RegisteredConstructors[typeof(TService)] = constructorResolver;
        }

        public SituationsContainer<SituationEnum> Build()
        {
            return new SituationsContainer<SituationEnum>(new InstanceProviderFactory<SituationEnum>(_registrations), _registrations);
        }
    }
}