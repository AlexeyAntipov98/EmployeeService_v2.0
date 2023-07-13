using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository
{
    public interface IEmployeeRepository
    {
        void CreateEmployee(Employee employee);
        Employee? GetEmployeeById(int id);
        void DeleteEmployeeById(int id);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(Employee employee);
    }
}
