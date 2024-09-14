using Situations.Core;
using Situations.Core.Providers;

namespace Situations.NSubsitute
{
    internal class InstanceProviderFactory<SituationEnum> : IInstanceProviderFactory where SituationEnum : Enum
    {
        private readonly Registrations<SituationEnum> _registrations;

        public InstanceProviderFactory(Registrations<SituationEnum> registrations)
        {
            _registrations = registrations;
        }

        public InstanceProvider GetInstanceProvider()
        {
            var registeredInstanceProvider = new RegisteredInstanceProvider(_registrations.RegisteredInstanceResolvers);
            var constructorProvider = new DefaultConstructorProvider(new RegisteredConstructorProvider(_registrations.RegisteredConstructors));

            registeredInstanceProvider
              .SetNext(new NSubstituteInstanceProvider())
              .SetNext(new DefaultInstanceProvider(constructorProvider, this))
              .SetNext(new UninitiliazedObjectProvider());

            return registeredInstanceProvider;
        }
    }
}
