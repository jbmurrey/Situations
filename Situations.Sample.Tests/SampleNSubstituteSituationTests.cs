using Situations.Core;

namespace Situations.Sample.Tests
{
    [TestClass]
    public class SampleNSubstituteSituationTest
    {
        private IConfiguredService<EmployeeCreationService, EmployeeCreationSituations> _configuredService;

        [TestInitialize]
        public void Init()
        {
            var situationContainer = SampleNSubstituteSituationConfiguration.GetSituationsContainer();
            _configuredService = situationContainer.GetConfiguredService<EmployeeCreationService>();
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsManagerAndEmployeeTryingToBeAddedDoesNotExist_EmployeeWasAdded()
        {
            //Arrange
            _configuredService.InvokeSituation(EmployeeCreationSituations.RequestorIsManager);
            _configuredService.InvokeSituation(EmployeeCreationSituations.EmployeeTryingToAddedDoesNotExist);

            //Act
            _configuredService.Service.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _configuredService.InvokeSituation(EmployeeCreationSituations.EmployeeWasAdded);
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsManagerAndEmployeeTryingToBeAddedDoesExist_EmployeeWasAdded()
        {
            //Arrange
            _configuredService.InvokeSituation(EmployeeCreationSituations.RequestorIsManager);
            _configuredService.InvokeSituation(EmployeeCreationSituations.EmployeeTryingToBeAddedExist);

            //Act
            _configuredService.Service.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _configuredService.InvokeSituation(EmployeeCreationSituations.EmployeeWasNotAdded);
        }

        [TestMethod]
        public void AddEmployee_GivenRequestIsNotManager_NotifyManager()
        {
            //Arrange
            _configuredService.InvokeSituation(EmployeeCreationSituations.RequesterIsNotManager);
            _configuredService.InvokeSituation(EmployeeCreationSituations.ManagerOfEmployeeIsFound);

            //Act
            _configuredService.Service.AddEmployee(TestingConstants.EmployeeId, TestingConstants.Employee);

            //Assert
            _configuredService.InvokeSituation(EmployeeCreationSituations.ManagerWasNotified);
        }


        [TestMethod]
        public void AddEmployee_GivenRequestIsManager_MangerWasNotNotified()
        {
            //Arrange
            _configuredService.InvokeSituation(EmployeeCreationSituations.RequestorIsManager);
            _configuredService.InvokeSituation(EmployeeCreationSituations.ManagerOfEmployeeIsFound);
            //Act
            _configuredService.Service.AddEmployee(TestingConstants.ManagerId, TestingConstants.Employee);

            //Assert
            _configuredService.InvokeSituation(EmployeeCreationSituations.ManagerWasNotNotified);
        }

    }
}