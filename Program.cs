using EmployeeService_v2._0.DataBase.Repository.Documents;
using EmployeeService_v2._0.DataBase.Repository.Employees;
using EmployeeService_v2._0.DataBase.Repository.Organizations;
using Microsoft.AspNetCore.Routing.Constraints;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string connectionString = "Data Source=DataBase/Employees.sqlite";
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(provider => new EmployeeRepository(connectionString));
        builder.Services.AddTransient<IDocumentRepository, DocumentRepository>(provider => new DocumentRepository(connectionString));
        builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>(provider => new OrganizationRepository(connectionString));
        builder.Services.AddMvc();
        var app = builder.Build();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "Documents",
            pattern: "Documents/{action}/{employeeId}/{docId?}",
            constraints: new { employeeId = new IntRouteConstraint() });
        app.MapControllerRoute(
            name: "Organizations",
            pattern: "Organizations/{action=Index}/{id?}/{emplId}",
            constraints: new { employeeId = new IntRouteConstraint() });
        app.Run();
    }
}