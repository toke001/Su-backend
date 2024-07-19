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
        /// <param name="kato"></param>
        /// <param name="year"></param>
        /// <returns></returns>        
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
    }
}
