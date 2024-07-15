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
                var entity = await _repo.GetFormsAsync(kato, year);
                var content = _repo.GenerateExcelFile(entity);
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyEntities.xlsx");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
