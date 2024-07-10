using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Data;
using WebServer.Interfaces;
using WebServer.Models;
using WebServer.Reposotory;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefKatoController : ControllerBase
    {
        private readonly IRefKato _repository;
        public RefKatoController(IRefKato repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetKatoByParentId([FromQuery] int parentId, [FromQuery] bool? getNested)
        {
            return Ok(await _repository.GetRefKatoAsync(parentId, getNested));
        }
        
        [HttpGet("kato/IsReportable")]
        public async Task<IActionResult> GetIsReportableStatus([FromQuery] int parentId)
        {
            return Ok(await _repository.IsReportable(parentId));
        }

        #region Street
        [HttpGet("street")]
        public async Task<IActionResult> GetRefStreetByKatoId([FromQuery] int id)
        {
            return Ok(await _repository.GetRefStreetByKatoId(id));
        }

        [HttpPost("street")]
        public async Task<IActionResult> GetRefStreetByKatoId([FromBody] Ref_Street row)
        {
            return Ok(await _repository.AddStreet(row));
        }

        [HttpPut("street")]
        public async Task<IActionResult> UpdateStreet([FromBody] Ref_Street row, [FromQuery] int id)
        {
            return Ok(await _repository.UpdateStreet(row, id));
        }

        [HttpDelete("street")]
        public ActionResult DeleteStreet([FromQuery] int id)
        {
            _repository.DeleteStreet(id);
            return NoContent();
        }
        #endregion

        #region Buildingbuilding
        [HttpGet("building")]
        public async Task<IActionResult> GetRefBuildingByStreetId([FromQuery] int id)
        {
            return Ok(await _repository.GetRefBuildingByStreetId(id));
        }

        [HttpPost("building")]
        public async Task<IActionResult> AddBuilding([FromBody] Ref_Building row)
        {
            return Ok(await _repository.AddBuilding(row));
        }

        [HttpPut("building")]
        public async Task<IActionResult> UpdateBuilding([FromBody] Ref_Building row, [FromQuery] int id)
        {
            return Ok(await _repository.UpdateBuilding(row, id));
        }

        [HttpDelete("building")]
        public ActionResult DeleteBuilding([FromQuery] int id)
        {
            _repository.DeleteBuilding(id);
            return NoContent();
        }
        #endregion
    }
}
