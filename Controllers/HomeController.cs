using EmployeeService_v2._0.DataBase.Repository;
using EmployeeService_v2._0.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text.Json;

namespace EmployeeService_v2._0.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository userRepos;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        public HomeController(IEmployeeRepository userRepos)
        {
            this.userRepos = userRepos;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return LocalRedirect("~/Home/GetAllEmployees");
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Json(userRepos.GetAllEmployees(), jsonOptions);
        }
        [HttpPost]
        public IActionResult CreateEmployee(Employee empl)
        {
            try
            {
                userRepos.CreateEmployee(empl);
            }
            catch (SqliteException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            return Json(empl.Id, jsonOptions);
        }
        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            var empl = userRepos.GetEmployeeById(id);
            return Json(empl, jsonOptions);
        }
        [HttpPost]
        public IActionResult UpdateEmployee(Employee empl)
        {
            userRepos.UpdateEmployee(empl);
            return Json(empl, jsonOptions);
        }
        [HttpPost]
        public IActionResult DeleteEmployeeById(int id)
        {
            try
            {
                userRepos.DeleteEmployeeById(id);
            }
            catch (SqliteException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            return Content($"Employee with id {id} is deleted");
        }
    }
}
