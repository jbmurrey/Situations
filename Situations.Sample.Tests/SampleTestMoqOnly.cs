using Moq;

namespace Situations.Sample.Tests
{
    [TestClass]
    public class SampleTestMoqOnly
    {
        private readonly Mock<IPositionRepository> _mockPositionRepository = new Mock<IPositionRepository>();
        private readonly Mock<IEmployeeService> _mockEmployeeService = new Mock<IEmployeeService>();
        private readonly Mock<INotificationService> _mockNotificationService = new Mock<INotificationService>();
        private EmployeeCreationService employeeCreationService;

        [TestInitialize]
        public void Init()
        {
          //  employeeCreationService = new EmployeeCreationService(_mockEmployeeService.Object, _mockPositionRepository.Object, _mockNotificationService.Object);
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsManagerAndEmployeeTryingToBeAddedDoesNotExist_EmployeeWasAdded()
        {
            //Arrange
            _mockPositionRepository.Setup((x) => x.IsManager(TestingConstants.ManagerId)).Returns(true);
            _mockEmployeeService.Setup(x => x.EmployeeExist(TestingConstants.EmployeeId)).Returns(true);

            //Act
            employeeCreationService.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _mockEmployeeService.Verify(x => x.AddEmployee(TestingConstants.Employee), Times.Once);
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsManagerAndEmployeeTryingToBeAddedDoesExist_EmployeeWasAdded()
        {
            //Arrange


            //Act

            //Assert

        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsNotManager_NotifyManager()
        {
            //Arrange


            //Act


            //Assert
        }

    }
}