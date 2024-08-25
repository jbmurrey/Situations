using Microsoft.Data.SqlClient;
using Moq;
using Situations.Core;

namespace Situations.Sample.Tests
{
    public static class SampleConfiguration
    {
        public static ISituationsContainer<EmployeeCreationSituations> GetSituationsContainer()
        {
            var builder = new SituationsBuilder<EmployeeCreationSituations>();

            builder
                .RegisterSituation<IPositionRepository>(EmployeeCreationSituations.RequestorIsManager)
                .OnCapture(mock => mock.Setup((x) => x.IsManager(TestingConstants.ManagerId)).Returns(true));
            builder
                .RegisterSituation<IPositionRepository>(EmployeeCreationSituations.RequesterIsNotManager)
                .OnCapture(mock => mock.Setup((x) => x.IsManager(TestingConstants.EmployeeId)).Returns(false));
            builder
                .RegisterSituation<IEmployeeService>(EmployeeCreationSituations.EmployeeTryingToAddedDoesNotExist)
                .OnCapture(mock => mock.Setup((x) => x.EmployeeExist(TestingConstants.EmployeeId)).Returns(false));
            builder
                .RegisterSituation<IEmployeeService>(EmployeeCreationSituations.EmployeeTryingToBeAddedAlreadyExist)
                .OnCapture(mock => mock.Setup((x) => x.EmployeeExist(TestingConstants.EmployeeId)).Returns(true));
            builder
                .RegisterSituation<IEmployeeService>(EmployeeCreationSituations.EmployeeWasAdded)
                .OnCapture(mock => mock.Verify(x => x.AddEmployee(TestingConstants.Employee), Times.Once));
            builder
                .RegisterSituation<IEmployeeService>(EmployeeCreationSituations.EmployeeWasNotAdded)
                .OnCapture(mock => mock.Verify(x => x.AddEmployee(TestingConstants.Employee), Times.Never));
            builder
                .RegisterSituation<INotificationService>(EmployeeCreationSituations.ManagerWasNotified)
                .OnCapture(mock => mock.Verify(x => x.Notify(TestingConstants.ManagerId, It.IsAny<string>()), Times.Once));
            builder
                .RegisterSituation<INotificationService>(EmployeeCreationSituations.ManagerWasNotNotified)
                .OnCapture(mock => mock.Verify(x => x.Notify(TestingConstants.ManagerId, It.IsAny<string>()), Times.Never));
            builder
            .RegisterSituation<IPositionRepository>(EmployeeCreationSituations.ManagerOfEmployeeIsFound)
            .OnCapture(mock => mock.Setup(x => x.GetManagerOf(TestingConstants.EmployeeId)).Returns(TestingConstants.ManagerId));

            // if you want to replace Mocked object
            // builder.RegisterInstance<EmployeeService, IEmployeeService>(() => new EmployeeService());


            return builder.Build();
        }
    }
}
