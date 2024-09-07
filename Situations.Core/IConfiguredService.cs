namespace Situations.Core
{
    public interface IConfiguredService<TService, SituationEnum>
        where TService : class
        where SituationEnum : Enum
    {
        public TService Service { get; }
        void InvokeSituation(SituationEnum situationEnum);
    }
}