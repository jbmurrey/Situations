using System.Reflection;

namespace Situations.Core
{
    public class InstanceProvider<SituationEnum> : IInstanceProvider<SituationEnum> where SituationEnum : Enum
    {
        private readonly IInstanceProvider<SituationEnum> _innerHandler;
        private readonly IEnumerable<IRegisteredSituation<SituationEnum>> _situationRegistrations;

        public InstanceProvider(IInstanceProvider<SituationEnum> innerHandler, IEnumerable<IRegisteredSituation<SituationEnum>> situationRegistrations)
        {
            _innerHandler = innerHandler;
            _situationRegistrations = situationRegistrations;
        }

        public object GetInstance(Type instanceType)
        {
            var constructors = instanceType.GetConstructors();
            var constructorParameters = constructors[0].GetParameters();

            var situationDictionary =
                _situationRegistrations
                .DistinctBy(x => x.RegistrationType)
                .ToDictionary(x => x.RegistrationType.Name, x => x.Instance);

            var parameters = GetParameters(_situationRegistrations, constructorParameters, situationDictionary);

            return constructors[0].Invoke(parameters);
        }

        private object GetInstance(Type instanceType, IEnumerable<IRegisteredSituation<SituationEnum>> situations, Dictionary<string, object> situationsDictionary)
        {
            var constructors = instanceType.GetConstructors();
            var constructorParameters = constructors.Length == 0 ? null : constructors[0].GetParameters();

            if (constructors.Length == 0 || constructorParameters!.Length == 0)
            {
                return _innerHandler.GetInstance(instanceType);
            }

            var parameters = GetParameters(situations, constructorParameters!, situationsDictionary);

            return constructors[0].Invoke(parameters.ToArray());
        }

        private object[] GetParameters(IEnumerable<IRegisteredSituation<SituationEnum>> situations, ParameterInfo[] constructorParameters, Dictionary<string, object> situationDictionary)
        {
            var parameters = new List<object>();

            foreach (var constructorParameter in constructorParameters)
            {
                if (situationDictionary.TryGetValue(constructorParameter.ParameterType.Name, out var instance))
                {
                    parameters.Add(instance);
                }
                else
                {
                    parameters.Add(GetInstance(constructorParameter.ParameterType, situations, situationDictionary));
                }
            }

            return parameters.ToArray();
        }
    }
}
