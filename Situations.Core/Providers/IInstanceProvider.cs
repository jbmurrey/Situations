using Situations.Core.Monads;

namespace Situations.Core.InstanceProviders
{
    public abstract class IInstanceProvider
    {
        protected IInstanceProvider _instanceProvider;
        public IInstanceProvider SetNext(IInstanceProvider instanceProvider)
        {
            _instanceProvider = instanceProvider;
            return instanceProvider;
        }

        public abstract Result<object> TryGetInstance(Type instanceType);
    }
}
