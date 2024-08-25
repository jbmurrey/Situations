namespace Situations.Core
{
    public interface ISituationsContainer<SituationEnum> where SituationEnum : Enum
    {
        IConfiguredService<T, SituationEnum> GetConfiguredService<T>() where T : class;
    }
}