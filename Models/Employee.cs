namespace EmployeeService_v2._0.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Phone { get; set; } = "";
        public int OrganizationId { get; set; }
        public int DepartamentId { get; set; }
        public static bool operator ==(Employee? a, Employee? b)
        {
            if (a is not null && b is not null)
            {
                if (a.Id == b.Id
                    && a.Name == b.Name
                    && a.Surname == b.Surname
                    && a.Phone == b.Phone
                    && a.OrganizationId == b.OrganizationId
                    && a.DepartamentId == b.DepartamentId)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool operator !=(Employee? a, Employee? b)
        {
            return !(a == b);
        }
    }
}
