using Moq;
using Situations.Core;
using Situations.Core.Providers;

namespace Situations.Moq
{
    internal class MockSingletonFactory<SituationEnum> where SituationEnum : Enum
    {
        private static MockOfTProvider<SituationEnum> _mockOfTProvider = new MockOfTProvider<SituationEnum>();
        private static object _mockOfT;    

        public static Mock<T> GetMock<T>() where T : class 
        {
            return (Mock<T>)_mockOfT ??= (Mock<T>)GetMockObject();
        }

        private static object GetMockObject()
        {
            return new _mock
        }
    }
}
