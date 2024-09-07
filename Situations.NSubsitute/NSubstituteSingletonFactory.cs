using NSubstitute;

namespace Situations.NSubsitute
{
    internal static class NSubstituteSingletonFactory<T> where T : class
    {
        private static T? _Instance;
        public static T Instance
        {
            get
            {
                return _Instance ??= Substitute.For<T>();
            }
        }
    }
}
