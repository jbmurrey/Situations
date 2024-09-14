using Situations.Core.Exceptions;

namespace Situations.Core.Providers
{
    public abstract class InstanceProvider
    {
        protected InstanceProvider? _instanceProvider;
        public InstanceProvider SetNext(InstanceProvider instanceProvider)
        {
            _instanceProvider = instanceProvider;
            return instanceProvider;
        }

        public virtual object GetInstance(Type instanceType)
        {
            try
            {
                if (_instanceProvider != null)
                {
                    return _instanceProvider.GetInstance(instanceType);
                }

                var message = $"Unable to create instance of type: {instanceType}, no suitable way of creating an instance of this class was found";

                throw new InstanceCreationException(message);
            }
            catch (Exception ex) when (ex is not InstanceCreationException)
            {
                throw new InstanceCreationException($"Unable to create instance of type: {instanceType}", ex);
            }
        }
    }
}
