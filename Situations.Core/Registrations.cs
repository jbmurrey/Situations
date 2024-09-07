using System.Reflection;

namespace Situations.Core
{
    public class Registrations<SituationEnum> where SituationEnum : Enum
    {
        public Registrations()
        {
            RegisteredInstanceResolvers = new();
            RegisteredConstructors = new();
            RegisteredSituations = new();
        }

        public Dictionary<Type, Func<object>> RegisteredInstanceResolvers { get; }
        public Dictionary<Type, Func<Type, ConstructorInfo>> RegisteredConstructors { get; }
        public Dictionary<SituationEnum, IRegisteredSituation<SituationEnum>> RegisteredSituations { get; }
    }
}
