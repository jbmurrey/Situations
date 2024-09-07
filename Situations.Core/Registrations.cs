using Situations.Core;
using System.Reflection;

namespace Situations.Moq
{
    public class Registrations<SituationEnum> where SituationEnum : Enum
    {
        public Registrations()
        {
            RegisteredInstances = new();
            RegisteredConstructors = new();
            RegisteredSituations = new();
        }

        public Dictionary<Type, Func<object>> RegisteredInstances { get; }
        public Dictionary<Type, Func<Type, ConstructorInfo>> RegisteredConstructors { get; }
        public Dictionary<SituationEnum, IRegisteredSituation<SituationEnum>> RegisteredSituations { get; }
    }
}
