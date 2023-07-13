namespace EmployeeService_v2._0.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Phone { get; set; } = "";
        public int PassportID { get; set; } = -1;
        public int CompanyID { get; set; } = -1;
    }
}
