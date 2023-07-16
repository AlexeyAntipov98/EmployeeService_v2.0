using EmployeeService_v2._0.DataBase.Repository.Documents;
using EmployeeService_v2._0.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace EmployeeService_v2._0.Controllers
{
    public class DocumentsController : Controller
    {
        private IDocumentRepository docRepos;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        public DocumentsController(IDocumentRepository docRepos)
        {
            this.docRepos = docRepos;
        }
        [HttpGet]
        public IActionResult GetEmployeeDocuments(int? employeeId)
        {
            if (employeeId is null)
            {
                return new BadRequestResult();
            }
            return Json(docRepos.GetEmployeeDocuments(employeeId.Value));
        }
        [HttpPost]
        public IActionResult CreateDocument(Document document)
        {
            docRepos.CreateDocument(document);
            return Json(document.Id, jsonOptions);
        }
        [HttpPost]
        public IActionResult DeleteDocument(int docId)
        {
            docRepos.DeleteDocumentByID(docId); 
            return Content($"Document with id {docId} is deleted");
        }
    }
}
