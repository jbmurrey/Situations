namespace Situations.Core
{
    public interface IRegisteredSituation<SituationEnum>
        where SituationEnum : Enum
    {
        public Type RegistrationType { get; }
        SituationEnum Situation { get; }
        object Instance { get; }
        void Capture();
    }
}
