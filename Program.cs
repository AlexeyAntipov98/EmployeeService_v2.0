using EmployeeService_v2._0.DataBase.Repository;
using EmployeeService_v2._0.Models;
using Microsoft.AspNetCore.Routing.Constraints;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string connectionString = "Data Source=DataBase/Employees.sqlite";
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(provider => new EmployeeRepository(connectionString));
        builder.Services.AddTransient<IDocumentRepository, DocumentRepository>(provider => new DocumentRepository(connectionString));
        builder.Services.AddMvc();
        var app = builder.Build();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "Documents",
            pattern: "Documents/{action}/{employeeId}/{docId?}",
            constraints: new { employeeId = new IntRouteConstraint() });
        app.Run();
    }
}