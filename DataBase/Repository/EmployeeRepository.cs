using Dapper;
using EmployeeService_v2._0.Models;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SqlTypes;

namespace EmployeeService_v2._0.DataBase.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string connectionString = string.Empty;
        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void CreateEmployee(Employee empl)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery;
                sqlQuery = @"SELECT id FROM Employees WHERE Name = @Name AND Surname = @Surname";
                int? emplId = db.Query<int?>(sqlQuery, empl).FirstOrDefault();
                if (emplId is null)
                {
                    sqlQuery = @"INSERT INTO Employees 
                                    (Name, Surname, Phone, CompanyID, PassportID)
                                VALUES
                                    (@Name, @Surname, @Phone, @CompanyID, @PassportID)";
                    db.Execute(sqlQuery, empl);
                    sqlQuery = @"SELECT id FROM Employees WHERE Name = @Name AND Surname = @Surname";
                    emplId = db.Query<int?>(sqlQuery, empl).FirstOrDefault();
                    if (emplId is not null)
                    {
                        empl.Id = emplId.Value;
                    }
                    else
                    {
                        throw new SqliteException("Failed to create user", 500);
                    }
                }
                else
                {
                    throw new SqliteException("This employee already exists", 418);
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Employee? GetEmployeeById(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Employee>("SELECT Id, Name, Surname, Phone, CompanyID, PassportID FROM Employees WHERE id = @id", new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Employee>("SELECT Id, Name, Surname, Phone, CompanyID, PassportID FROM Employees").ToList();
            }
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
