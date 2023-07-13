using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository
{
    public interface IEmployeeRepository
    {
        void CreateEmployee(Employee employee);
        void Delete(int id);
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetAllEmployees();
        void Update(Employee employee);
    }
}
