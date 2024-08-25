using Situations.Core.Exceptions;

namespace Situations.Core
{
    public class ConfiguredService<TService, SituationEnum> : IConfiguredService<TService, SituationEnum>
        where TService : class
        where SituationEnum : Enum
    {
        private readonly IEnumerable<IRegisteredSituation<SituationEnum>> _situations;

        public ConfiguredService(TService instance, IEnumerable<IRegisteredSituation<SituationEnum>> situations)
        {
            Instance = instance;
            _situations = situations;
        }

        public TService Instance { get; }

        public void Capture(SituationEnum situationEnum)
        {
            IRegisteredSituation<SituationEnum> situation;

            try
            {
                situation = _situations.First(x => x.Situation.Equals(situationEnum));
            }
            catch (Exception ex)
            {
                throw new UnregisteredSituationException($"{situationEnum} was not registered", ex);
            }

            situation.Capture();
        }

        public static implicit operator TService(ConfiguredService<TService, SituationEnum> instance)
        {
            return instance.Instance;
        }
    }
}
