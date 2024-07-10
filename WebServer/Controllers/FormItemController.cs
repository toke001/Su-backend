using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormItemController : ControllerBase
    {
        private readonly IFormItem _repo;
        public FormItemController(IFormItem repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetForms([FromQuery] Guid formid)
        {
            try
            {
                return Ok(await _repo.GetForms(formid));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ApprovedFormItem aForm)
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
        public async Task<IActionResult> Update(ApprovedFormItem aForm)
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
