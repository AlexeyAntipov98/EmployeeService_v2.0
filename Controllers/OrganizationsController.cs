using EmployeeService_v2._0.DataBase.Repository.Organizations;
using EmployeeService_v2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace EmployeeService_v2._0.Controllers
{
    public class OrganizationsController : Controller
    {
        private IOrganizationRepository orgRepos;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        public OrganizationsController(IOrganizationRepository orgRepos)
        {
            this.orgRepos = orgRepos;
        }
        public IActionResult Index()
        {
            return LocalRedirect("~/Organizations/GetAll");
        }
        [HttpPost]
        public IActionResult Create(Organization org)
        {
            orgRepos.Create(org);
            return Json(org.Id, jsonOptions);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            orgRepos.DeleteById(id);
            return Content($"Organization with id {id} is deleted");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(orgRepos.GetAll(), jsonOptions);
        }
        [HttpGet]
        public IActionResult GetAllEmployeesByOrgId(int id)
        {
            return Json(orgRepos.GetAllEmployeesByOrgId(id), jsonOptions);
        }
    }
}
