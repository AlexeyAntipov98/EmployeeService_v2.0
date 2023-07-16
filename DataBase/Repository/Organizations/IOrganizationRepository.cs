using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository.Organizations
{
    public interface IOrganizationRepository
    {
        public void Create(Organization organization);
        public void Update(Organization organization);
        public void DeleteById(int id);
        public IEnumerable<Organization> GetAll();
        public IEnumerable<Employee> GetAllEmployeesByOrgId(int orgId);
        public void DeleteEmployeeFromOrg(int orgId, int emplId);

    }
}
