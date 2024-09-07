using Situations.Core;
using Situations.Core.InstanceProviders;
using Situations.Core.Providers;

namespace Situations.Moq
{
    internal class InstanceProviderFactory<SituationEnum> : IInstanceProviderFactory where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations;

        public InstanceProviderFactory(Registrations<SituationEnum> registrations)
        {
            _registrations = registrations;
        }

        public IInstanceProvider GetInstanceProvider()
        {
            var registeredInstanceHandler = new RegisteredInstanceHandler(_registrations.RegisteredInstanceResolvers);
            var moqInstanceHandler = new MoqInstanceHandler(_registrations.RegisteredInstanceResolvers);
            var constructorProvider = new ConstructorProvider(new MoqConstructorProvider(_registrations.RegisteredConstructors));
            var parameterProvider = new ParameterProvider(_registrations.RegisteredInstanceResolvers, constructorProvider, this);
            var instanceProvider = new InstanceProvider(constructorProvider, parameterProvider);

                registeredInstanceHandler
                  .SetNext(moqInstanceHandler)
                  .SetNext(instanceProvider)
                  .SetNext(new FaultyInstanceProvider());

            return registeredInstanceHandler;
        }
    }
}
