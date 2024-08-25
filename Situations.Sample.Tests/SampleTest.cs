using Situations.Core;

namespace Situations.Sample.Tests
{
    [TestClass]
    public class SampleTest
    {
        private IConfiguredService<EmployeeCreationService, EmployeeCreationSituations> _configuredService;

        [TestInitialize]
        public void Init()
        {
            var situationContainer = SampleConfiguration.GetSituationsContainer();
            _configuredService = situationContainer.GetConfiguredService<EmployeeCreationService>();
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsManagerAndEmployeeTryingToBeAddedDoesNotExist_EmployeeWasAdded()
        {
            //Arrange
            _configuredService.Capture(EmployeeCreationSituations.RequestorIsManager);
            _configuredService.Capture(EmployeeCreationSituations.EmployeeTryingToAddedDoesNotExist);

            //Act
            _configuredService.Instance.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _configuredService.Capture(EmployeeCreationSituations.EmployeeWasAdded);
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsManagerAndEmployeeTryingToBeAddedDoesExist_EmployeeWasAdded()
        {
            //Arrange
            _configuredService.Capture(EmployeeCreationSituations.RequestorIsManager);
            _configuredService.Capture(EmployeeCreationSituations.EmployeeTryingToBeAddedAlreadyExist);

            //Act
            _configuredService.Instance.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _configuredService.Capture(EmployeeCreationSituations.EmployeeWasNotAdded);
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsNotManager_NotifyManager()
        {
            //Arrange
            _configuredService.Capture(EmployeeCreationSituations.RequesterIsNotManager);
            _configuredService.Capture(EmployeeCreationSituations.ManagerOfEmployeeIsFound);

            //Act
            _configuredService.Instance.AddEmployee(TestingConstants.EmployeeId, TestingConstants.Employee);

            //Assert
            _configuredService.Capture(EmployeeCreationSituations.ManagerWasNotified);
        }


        [TestMethod]
        public void AddEmployee_GivenRequestIsManager_MangerWasNotNotified()
        {
            //Arrange
            _configuredService.Capture(EmployeeCreationSituations.RequestorIsManager);
            _configuredService.Capture(EmployeeCreationSituations.ManagerOfEmployeeIsFound);
            //Act
            _configuredService.Instance.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _configuredService.Capture(EmployeeCreationSituations.ManagerWasNotNotified);
        }

    }
}