using EmployeeService_v2._0.Models;

namespace EmployeeService_v2._0.DataBase.Repository.Documents
{
    public interface IDocumentRepository
    {
        public IEnumerable<Document> GetEmployeeDocuments(int employeeId);
        public void CreateDocument(Document document);
        public void DeleteDocumentByID(int id);
    }
}
