using Dapper;
using EmployeeService_v2._0.Models;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection.Metadata;

namespace EmployeeService_v2._0.DataBase.Repository.Organizations
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private string connectionString = string.Empty;
        public OrganizationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Create(Organization organization)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery;
                sqlQuery = @"SELECT id FROM Organizations WHERE Type = @Type AND Name = @Name AND INN = @INN";
                int? orgId = db.Query<int?>(sqlQuery, organization).FirstOrDefault();
                if (orgId is null)
                {
                    sqlQuery = @"INSERT INTO Organizations 
                                    (Type, Name, INN)
                                VALUES
                                    (@Type, @Name, @INN)";
                    db.Execute(sqlQuery, organization);
                    sqlQuery = @"SELECT id FROM Organizations WHERE Type = @Type AND Name = @Name AND INN = @INN";
                    orgId = db.Query<int?>(sqlQuery, organization).FirstOrDefault();
                    if (orgId is not null)
                    {
                        organization.Id = orgId.Value;
                    }
                }
            }
        }
        public void DeleteById(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = $"DELETE FROM Organizations WHERE id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public void Update(Organization organization)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Organization> GetAll()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Organization>("SELECT * FROM Organizations").ToList();
            }
        }
        public IEnumerable<Employee> GetAllEmployeesByOrgId(int orgId)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                return db.Query<Employee>("SELECT * FROM Employees WHERE OrganizationId = @orgId", new { orgId }).ToList();
            }
        }
        public void DeleteEmployeeFromOrg(int orgId, int emplId)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = $"UPDATE Employees SET OrganizationId = NULL WHERE orgId = @orgId AND Id = @emplId";
                db.Execute(sqlQuery, new { orgId, emplId });
            }
        }
    }
}
