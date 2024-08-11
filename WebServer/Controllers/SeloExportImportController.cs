using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Interfaces;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeloExportImportController : ControllerBase
    {
        private readonly ISeloExportImport _repo;

        public SeloExportImportController(ISeloExportImport repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Получение ексель файла с данными административно-территориальной единицы по КАТО и году с дочерними объектами
        /// </summary>
        /// <param name="parentKato"></param>
        /// <param name="year"></param>
        /// <returns></returns>        
        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel(string parentKato, int year)
        {
            try
            {
                var entities = await _repo.GetSeloTotalFormsByParentCodAsync(parentKato, year);
                var content = _repo.GenerateExcelFile(entities);
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"SeloForms_{year}.xlsx");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение пустого ексель файла с названиями колонок
        /// </summary>
        /// <returns></returns>
        [HttpGet("exportTemplate")]
        public IActionResult GetExportExcelTamplate()
        {
            try
            {
                var content = _repo.GenerateExcelTemplate();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SeloFormsTemplate.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportExcel(IFormFile file, string login, int year)
        {
            if (file == null || file.Length == 0) return BadRequest("File is empty!");
            try
            {
                return Ok(await _repo.ImportExcel(file, login, year));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
