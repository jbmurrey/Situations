using Moq;
using Situations.Core;

namespace Situations.Moq
{
    public class RegisteredSituation<SituationEnum, Service> : IRegisteredSituation<SituationEnum>
        where SituationEnum : Enum
        where Service : class
    {
        private readonly Mock<Service> _mock;

        public RegisteredSituation(SituationEnum situation, Mock<Service> registeredService)
        {
            Situation = situation;
            _mock = registeredService;
        }

        public SituationEnum Situation { get; }
        public Type RegistrationType => typeof(Service);
        public object Instance => _mock.Object;

        public RegisteredSituation<SituationEnum, Service> OnInvocation(Action<Mock<Service>> action)
        {
            _actionsToCapture += (sender, args) => action(_mock);
            return this;
        }

        public void Invoke()
        {
            _actionsToCapture?.Invoke(this, null!);
        }

        private event EventHandler<Action<Mock<Service>>>? _actionsToCapture;
    }
}
