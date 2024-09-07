using Situations.Core.Exceptions;

namespace Situations.Core
{
    public class ConfiguredService<TService, SituationEnum> : IConfiguredService<TService, SituationEnum>
        where TService : class
        where SituationEnum : Enum
    {
        private readonly Dictionary<SituationEnum, IRegisteredSituation<SituationEnum>> _situations;

        public ConfiguredService(TService instance, Dictionary<SituationEnum, IRegisteredSituation<SituationEnum>> situations)
        {
            Service = instance;
            _situations = situations;
        }

        public TService Service { get; }

        public void InvokeSituation(SituationEnum situationEnum)
        {
            IRegisteredSituation<SituationEnum> situation;

            try
            {
                situation = _situations[situationEnum];
            }
            catch (Exception ex)
            {
                throw new UnregisteredSituationException($"{situationEnum} was not registered", ex);
            }

            situation.Invoke();
        }

        public static implicit operator TService(ConfiguredService<TService, SituationEnum> instance)
        {
            return instance.Service;
        }
    }
}
