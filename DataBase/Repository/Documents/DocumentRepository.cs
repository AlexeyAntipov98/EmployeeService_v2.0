using Dapper;
using EmployeeService_v2._0.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace EmployeeService_v2._0.DataBase.Repository.Documents
{
    public class DocumentRepository : IDocumentRepository
    {
        private string connectionString = string.Empty;
        public DocumentRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IEnumerable<Document> GetEmployeeDocuments(int employeeId)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Document>("SELECT * FROM Documents WHERE EmployeeId = @employeeId", new { employeeId }).ToList();
            }
        }

        public void CreateDocument(Document document)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery;
                sqlQuery = @"SELECT id FROM Documents WHERE Type = @Type AND Serial= @Serial AND Number = @Number AND EmployeeId = @EmployeeId";
                int? docId = db.Query<int?>(sqlQuery, document).FirstOrDefault();
                if (docId is null)
                {
                    sqlQuery = @"INSERT INTO Documents 
                                    (Type, Serial, Number, EmployeeId)
                                VALUES
                                    (@Type, @Serial, @Number, @EmployeeId)";
                    db.Execute(sqlQuery, document);
                    sqlQuery = @"SELECT id FROM Documents WHERE Type = @Type AND Serial= @Serial AND Number = @Number AND EmployeeId = @EmployeeId";
                    docId = db.Query<int?>(sqlQuery, document).FirstOrDefault();
                    if (docId is not null)
                    {
                        document.Id = docId.Value;
                    }
                }
            }
        }

        public void DeleteDocumentByID(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = $"DELETE FROM Documents WHERE id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
