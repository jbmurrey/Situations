using Situations.Core;
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
            var registeredInstanceHandler = new RegisteredInstanceProvider(_registrations.RegisteredInstanceResolvers);
            var constructorProvider = new ConstructorProvider(new RegisteredConstructorProvider(_registrations.RegisteredConstructors));
            var instanceProvider = new InstanceProvider(constructorProvider, this);

            registeredInstanceHandler
              .SetNext(new MoqInstanceProvider())
              .SetNext(instanceProvider)
              .SetNext(new FaultyInstanceProvider());

            return registeredInstanceHandler;
        }
    }
}
