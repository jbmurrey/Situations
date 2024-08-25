namespace Situations.Core
{
    public interface IInstanceProvider<SituationEnum> where SituationEnum : Enum
    {
        object GetInstance(Type instanceType);
    }
}
