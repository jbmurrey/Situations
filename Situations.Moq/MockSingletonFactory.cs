using Moq;

namespace Situations.Moq
{
    internal static class MockSingletonFactory<T> where T : class
    {
        private static Mock<T>? _mockInstance;
        public static Mock<T> Instance
        {
            get
            {
                return _mockInstance ??= new Mock<T>();
            }
        }
    }
}
