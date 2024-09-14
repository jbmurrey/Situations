using System.Runtime.Serialization;

namespace Situations.Core.Providers
{
    public class UninitiliazedObjectProvider : InstanceProvider
    {
        public override object GetInstance(Type instanceType)
        {
            return FormatterServices.GetUninitializedObject(instanceType);
        }
    }
}
