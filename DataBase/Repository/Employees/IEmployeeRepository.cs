using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository.Employees
{
    public interface IEmployeeRepository
    {
        public void CreateEmployee(Employee employee);
        public Employee? GetEmployeeById(int id);
        public void DeleteEmployeeById(int id);
        public IEnumerable<Employee> GetAllEmployees();
        public void UpdateEmployee(Employee employee);
    }
}
