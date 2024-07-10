using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Dtos;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly Interfaces.IData _repo;
        public DataController(Interfaces.IData repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            return Ok(await _repo.Get(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<DataTableDto> data)
        {
            return Ok(await _repo.Update(data));
        }
    }
}
