using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository.Departaments
{
    public interface IDepartamentRepository
    {
        public void Create(Departament departament);
        public void Update(Departament departament);
        public void DeleteById(int id);
        public IEnumerable<Employee> GetAllEmployeesById(int depId);
    }
}
