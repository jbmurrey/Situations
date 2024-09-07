namespace Situations.Sample
{
    public class EmployeeCreationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionRepository _positionRepository;
        private readonly INotificationService _notificationService;
        private readonly ILoggingService _loggingService;
        private readonly MultipleConstructorParameterClass _multipleConstructorParameterClass;

        public EmployeeCreationService(IEmployeeService employeeService, IPositionRepository positionRepository, INotificationService notificationService, ILoggingService loggingService, MultipleConstructorParameterClass multiParamClass)
        {
            _employeeService = employeeService;
            _positionRepository = positionRepository;
            _notificationService = notificationService;
            _multipleConstructorParameterClass = multiParamClass;
            _loggingService = loggingService;
        }

        public EmployeeCreationService(EmployeeService employeeService, IPositionRepository positionRepository, INotificationService notificationService, ILoggingService loggingService, MultipleConstructorParameterClass multiParamClass)
        {
            _employeeService = employeeService;
            _positionRepository = positionRepository;
            _notificationService = notificationService;
            _loggingService = loggingService;
            _multipleConstructorParameterClass = multiParamClass;
        }

        public void AddEmployee(int requestUserId, Employee employee)
        {
            if (_positionRepository.IsManager(requestUserId))
            {
                if (!_employeeService.EmployeeExist(employee.Id))
                {
                    _loggingService.Log($"Adding employee {employee.Id}");
                    _employeeService.AddEmployee(employee);
                }
            }
            else
            {
                var managerId = _positionRepository.GetManagerOf(requestUserId);
                _notificationService.Notify(managerId, $"{requestUserId} is attempting to hire an Employee.");
            }
        }
    }
}
