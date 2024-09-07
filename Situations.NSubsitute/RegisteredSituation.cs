using Situations.Core;

namespace Situations.NSubsitute
{
    public class RegisteredSituation<SituationEnum, Service> : IRegisteredSituation<SituationEnum>
        where SituationEnum : Enum
        where Service : class
    {
        private readonly object _registeredService;
        public RegisteredSituation(SituationEnum situation, object registeredService)
        {
            Situation = situation;
            _registeredService = registeredService;
        }

        public SituationEnum Situation { get; }
        public Type RegistrationType => typeof(Service);
        public object Instance => _registeredService;

        public RegisteredSituation<SituationEnum, Service> OnInvocation(Action<Service> action)
        {
            _actionsToCapture += (sender, args) => action((Service)_registeredService);
            return this;
        }

        public void Invoke()
        {
            _actionsToCapture?.Invoke(this, null!);
        }

        private event EventHandler<Action<object>>? _actionsToCapture;
    }
}
