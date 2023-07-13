using EmployeeService_v2._0.DataBase.Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string connectionString = "Data Source=DataBase/Employees.sqlite";
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(provider => new EmployeeRepository(connectionString));
        builder.Services.AddMvc();
        var app = builder.Build();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
    }
}