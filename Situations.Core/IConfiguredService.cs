namespace Situations.Core
{
    public interface IConfiguredService<TService, SituationEnum>
        where TService : class
        where SituationEnum : Enum
    {
        public TService Instance { get; }
        void Capture(SituationEnum situationEnum);
    }
}