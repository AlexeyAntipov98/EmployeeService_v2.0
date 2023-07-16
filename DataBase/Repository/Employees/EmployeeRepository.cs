using Dapper;
using EmployeeService_v2._0.Models;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SqlTypes;

namespace EmployeeService_v2._0.DataBase.Repository.Employees
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
                sqlQuery = @"SELECT id FROM Employees WHERE Name = @Name AND Surname = @Surname AND OrganizationId = @OrganizationId AND @DepartamentId = @DepartamentId";
                int? emplId = db.Query<int?>(sqlQuery, empl).FirstOrDefault();
                if (emplId is null)
                {
                    sqlQuery = @"INSERT INTO Employees 
                                    (Name, Surname, Phone, OrganizationId, DepartamentId)
                                VALUES
                                    (@Name, @Surname, @Phone, @OrganizationId, @DepartamentId)";
                    db.Execute(sqlQuery, empl);
                    sqlQuery = @"SELECT id FROM Employees WHERE Name = @Name AND Surname = @Surname AND OrganizationId = @OrganizationId AND DepartamentId = @DepartamentId";
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

        public Employee? GetEmployeeById(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Employee>("SELECT * FROM Employees WHERE id = @id", new { id }).FirstOrDefault();
            }
        }

        public void DeleteEmployeeById(int id)
        {
            var empl = GetEmployeeById(id);
            if (empl is null)
            {
                throw new SqliteException("Employee not found", 400);
            }
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = $"DELETE FROM Employees WHERE id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Employee>("SELECT * FROM Employees").ToList();
            }
        }
        /*
         * Если передадим null, то будет считать, что значение удалено
         */
        public void UpdateEmployee(Employee employee)
        {
            var emplFromDB = GetEmployeeById(employee.Id);
            if (emplFromDB is not null && employee != emplFromDB) // заменить на отслеживание изменений в нормальной проекте
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    db.Open();
                    /*Будет больше 50 полей, можно задуматься о рефлексии*/
                    List<string> updatebleFields = new List<string>();
                    if (!employee.Name.Equals(emplFromDB.Name))
                        updatebleFields.Add("Name=@Name");

                    if (!employee.Surname.Equals(emplFromDB.Surname))
                        updatebleFields.Add("Surname=@Surname");

                    if (!employee.Phone.Equals(emplFromDB.Phone))
                        updatebleFields.Add("Phone=@Phone");

                    if (employee.OrganizationId != emplFromDB.OrganizationId)
                        updatebleFields.Add("OrganizationId=@OrganizationId");

                    if (employee.DepartamentId != emplFromDB.DepartamentId)
                        updatebleFields.Add("DepartamentId=@DepartamentId");

                    if (updatebleFields.Count != 0)
                    {
                        string sqlQuery = $"UPDATE Employees SET {string.Join(", ", updatebleFields)} WHERE Id = @Id";
                        db.Execute(sqlQuery, employee);
                    }
                }
            }
        }
    }
}
