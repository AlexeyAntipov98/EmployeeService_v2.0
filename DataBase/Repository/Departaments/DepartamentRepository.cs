using Dapper;
using EmployeeService_v2._0.Models;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Security.Cryptography;

namespace EmployeeService_v2._0.DataBase.Repository.Departaments
{
    public class DepartamentRepository : IDepartamentRepository
    {
        private string connectionString = string.Empty;
        public DepartamentRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Create(Departament departament)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery;
                sqlQuery = @"SELECT id FROM Departaments WHERE Name = @Name AND OrganizationId = @OrganizationId";
                int? depId = db.Query<int?>(sqlQuery, departament).FirstOrDefault();
                if (depId is null)
                {
                    sqlQuery = @"INSERT INTO Departaments 
                                    (Name, OrganizationId)
                                VALUES
                                    (@Name, @OrganizationId)";
                    db.Execute(sqlQuery, departament);
                    sqlQuery = @"SELECT id FROM Departaments WHERE Name = @Name AND OrganizationId = @OrganizationId";
                    depId = db.Query<int?>(sqlQuery, departament).FirstOrDefault();
                    if (depId is not null)
                    {
                        departament.Id = depId.Value;
                    }
                }
            }
        }

        public void DeleteById(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "DELETE FROM Departaments WHERE id = @id";
                db.Execute(sqlQuery, new { id });
                sqlQuery = "UPDATE Employees SET DepartamentId = NULL WHERE DepartamentId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public IEnumerable<Employee> GetAllEmployeesById(int depId)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Employee>("SELECT * FROM Employees WHERE OrganizationId = @orgId", new { depId }).ToList();
            }
        }

        public void Update(Departament departament)
        {
            if (departament.Id != 0)
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    db.Open();
                    string sqlQuery = @"UPDATE Departaments SET Name = @Name, Phone = @Phone WHERE Id = @Id";
                    db.Execute(sqlQuery, departament);
                }
            }
        }
    }
}
