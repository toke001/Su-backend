using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormItemColumnController : ControllerBase
    {
        private readonly IFormItemColumn _repo;
        public FormItemColumnController(IFormItemColumn repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetForms([FromQuery] Guid tabId)
        {
            try
            {
                return Ok(await _repo.GetForms(tabId));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ApprovedFormItemColumnServDto aForm)
        {
            try
            {
                return Ok(await _repo.Add(aForm));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApprovedFormItemColumnServDto aForm)
        {
            try
            {
                return Ok(await _repo.Update(aForm));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                return Ok(await _repo.Delete(id));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }
    }
}
