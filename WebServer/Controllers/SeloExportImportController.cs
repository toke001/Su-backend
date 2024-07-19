using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Interfaces;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeloExportImportController : ControllerBase
    {
        private readonly ISeloExportImport _repo;

        public SeloExportImportController(ISeloExportImport repo)
        {
            _repo = repo;
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel(string kato, int year)
        {
            try
            {
                var entities = await _repo.GetSeloTotalFormsAsync(kato, year);
                var content = _repo.GenerateExcelFile(entities);
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"SeloForms_{year}.xlsx");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
