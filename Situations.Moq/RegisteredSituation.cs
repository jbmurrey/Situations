using Moq;
using Situations.Core;

namespace Situations.Moq
{
    public class RegisteredSituation<SituationEnum, Service> : IRegisteredSituation<SituationEnum>
        where SituationEnum : Enum
        where Service : class
    {
        private event EventHandler<Action<Mock<Service>>>? _actionsToCapture;

        public RegisteredSituation(SituationEnum situation, Mock<Service> registeredService)
        {
            Situation = situation;
            Mock = registeredService;
        }

        public SituationEnum Situation { get; }
        public Type RegistrationType => typeof(Service);
        public object Instance => Mock.Object;
        internal Mock<Service> Mock;

        public RegisteredSituation<SituationEnum, Service> OnInvocation(Action<Mock<Service>> action)
        {
            _actionsToCapture += (sender, args) => action(Mock);
            return this;
        }

        public void Capture()
        {
            _actionsToCapture?.Invoke(this, null!);
        }
    }
}
