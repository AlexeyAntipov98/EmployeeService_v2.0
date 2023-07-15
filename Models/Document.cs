namespace EmployeeService_v2._0.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Serial { get; set; } = "";
        public string Number { get; set; } = "";
        public int EmployeeId { get; set; }
    }
}
