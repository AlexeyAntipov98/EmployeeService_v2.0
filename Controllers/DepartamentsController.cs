using EmployeeService_v2._0.DataBase.Repository.Departaments;
using EmployeeService_v2._0.DataBase.Repository.Organizations;
using EmployeeService_v2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace EmployeeService_v2._0.Controllers
{
    public class DepartamentsController : Controller
    {
        private IDepartamentRepository depRepos;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        public DepartamentsController(IDepartamentRepository depRepos)
        {
            this.depRepos = depRepos;
        }
        [HttpPost]
        public IActionResult Create(Departament departament)
        {
            depRepos.Create(departament);
            return Json(departament.Id, jsonOptions);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            depRepos.DeleteById(id);
            return Content($"Departament with id {id} is deleted");
        }

        [HttpGet]
        public IActionResult GetAllEmployeesByDepId(int id)
        {
            return Json(depRepos.GetAllEmployeesById(id), jsonOptions);
        }
        [HttpGet]
        public IActionResult Update(Departament departament)
        {
            depRepos.Update(departament);
            return Json(depRepos, jsonOptions);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
