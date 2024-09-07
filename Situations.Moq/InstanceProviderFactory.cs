using Situations.Core;

namespace Situations.Moq
{
    internal static class InstanceProviderFactory
    {
        public static IInstanceProvider GetInstanceProvider<SituationEnum>(Registrations<SituationEnum> registrations) where SituationEnum : Enum
        {
            var moqInstanceProvider = new MoqInstanceProvider(registrations.RegisteredInstanceResolvers);
            var constructorProvider = new DefaultConstructorProvider(new MoqConstructorProvider(registrations.RegisteredConstructors));
            var parameterProvider = new DefaultParameterProvider(registrations.RegisteredInstanceResolvers, constructorProvider, moqInstanceProvider);

            return new DefaultInstanceProvider(moqInstanceProvider, constructorProvider, parameterProvider);
        }
    }
}
