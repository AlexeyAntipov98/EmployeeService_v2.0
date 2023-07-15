using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetEmployeeDocuments(int employeeId);
        public void CreateDocument(Document document);
        public void DeleteDocumentByID(int id);
    }
}
