using Moq;
using Situations.Core;

namespace Situations.Moq
{
    internal class MoqInstanceProvider<SituationEnum> : IInstanceProvider<SituationEnum> where SituationEnum : Enum
    {
        private readonly Dictionary<Type, Func<object>> _registeredInstances;

        public MoqInstanceProvider(IEnumerable<IRegisteredInstance> registeredInstances)
        {
            _registeredInstances = registeredInstances
                .Reverse()
                .DistinctBy(x => x.RegisteredType)
                .ToDictionary(x => x.RegisteredType, x => x.InstanceResolver);
        }

        public object GetInstance(Type instanceType)
        {

            if (_registeredInstances.TryGetValue(instanceType, out var instanceResolver))
            {
                return instanceResolver();
            }

            var mockType = typeof(Mock<>).MakeGenericType(instanceType);
            dynamic mockObject = Activator.CreateInstance(mockType)!;

            return mockObject.Object;
        }
    }
}
