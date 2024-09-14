using Moq;
using Situations.Core;
using Situations.Core.Providers;
using System.Reflection;

namespace Situations.Moq
{
    public class MoqSituationsBuilder<SituationEnum> where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations = new();
        private readonly IInstanceProviderFactory _instanceProviderFactory;
        private readonly IConstructorProvider _constructorProvider;
        public MoqSituationsBuilder()
        {
            _instanceProviderFactory = new InstanceProviderFactory<SituationEnum>(_registrations);
            _constructorProvider = new DefaultConstructorProvider(new RegisteredConstructorProvider(_registrations.RegisteredConstructors));

        }
        public RegisteredSituation<SituationEnum, TService> RegisterSituation<TService>(SituationEnum situation) where TService : class
        {
            var mock = MockSingletonFactory<TService>.GetMock(_registrations);
            var sitatuion = new RegisteredSituation<SituationEnum, TService>(situation, mock);

            _registrations.RegisteredInstanceResolvers[typeof(TService)] = () => mock.Object;
            _registrations.RegisteredSituations[situation] = sitatuion;

            return sitatuion;
        }

        public void RegisterInstance<Service>(Func<Service> instanceResolver)
            where Service : class
        {
            _registrations.RegisteredInstanceResolvers[typeof(Service)] = () => instanceResolver()!;
        }

        public void RegisterConstructor<Service>(Func<Type, ConstructorInfo> constructorResolver) where Service : class
        {
            _registrations.RegisteredConstructors[typeof(Service)] = constructorResolver;
        }

        public SituationsContainer<SituationEnum> Build()
        {
            return new SituationsContainer<SituationEnum>(_instanceProviderFactory, _registrations.RegisteredSituations);
        }
    }
}