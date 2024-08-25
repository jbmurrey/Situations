namespace Situations.Sample.Tests
{
    public enum EmployeeCreationSituations
    {
        RequestorIsManager,
        RequesterIsNotManager,
        EmployeeTryingToBeAddedAlreadyExist,
        EmployeeTryingToAddedDoesNotExist,
        EmployeeWasAdded,
        EmployeeWasNotAdded,
        ManagerWasNotified,
        ManagerWasNotNotified,
        ManagerOfEmployeeIsFound
    }
}
