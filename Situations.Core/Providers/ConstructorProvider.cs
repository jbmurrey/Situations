using System.Reflection;

namespace Situations.Core.Providers
{
    public class ConstructorProvider : IConstructorProvider
    {
        private readonly IConstructorProvider _constructorProvider;

        public ConstructorProvider(IConstructorProvider constructorProvider)
        {
            _constructorProvider = constructorProvider;
        }

        public bool TryGetConstructorInfo(Type type, out ConstructorInfo? constructorInfo)
        {
            constructorInfo = null;

            try
            {
                if (_constructorProvider.TryGetConstructorInfo(type, out constructorInfo))
                {
                    return true;
                }

                var constructors = type.GetConstructors();

                if (constructors.Length != 0)
                {
                    constructorInfo = constructors[0];
                    return true;
                }


                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
