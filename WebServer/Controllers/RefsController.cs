using Microsoft.AspNetCore.Mvc;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefsController : ControllerBase
    {
        private readonly IRefs _repo;
        public RefsController(IRefs repo)
        {
            _repo = repo;
        }

        [HttpGet("refRolesList")]
        public async Task<IActionResult> GetRefRolesList()
        {
            return Ok(await _repo.GetRefRolesList());
        }

        [HttpGet("datatypes")]
        public ActionResult GetDataTypes()
        {
            return Ok(_repo.GetDataTypes());
        }

        [HttpGet("univerList")]
        public async Task<ActionResult> GetRefUniverList()
        {
            try
            {
                return Ok(await _repo.GetRefUniverList());
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("GetRefUniverById")]
        public async Task<ActionResult> GetRefUniverById(Guid Id)
        {
            try
            {
                return Ok(await _repo.GetRefUniverById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddRefUniver")]
        public async Task<IActionResult> AddRefUniver([FromBody] Universal_Refference model)
        {
            try
            {
                var res = await _repo.AddRefUniver(model);
                return RedirectToAction(nameof(GetRefUniverById), new {Id = res.Id});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateRefUniver")]
        public async Task<IActionResult> UpdateRefUniver([FromBody] Universal_Refference model)
        {
            try
            {
                return Ok(await _repo.UpdateRefUniver(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRefUniver")]
        public async Task<IActionResult> DeleteRefUniver(Guid Id)
        {
            try
            {
                return Ok(await _repo.DeleteRefUniver(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("businesList")]
        public async Task<ActionResult> GetBusinesDictList()
        {
            return Ok(await _repo.GetBusinesDictList());
        }

        [HttpGet("GetBusinesDictById")]
        public async Task<ActionResult> GetBusinesDictById(Guid Id)
        {
            try
            {
                return Ok(await _repo.GetBusinesDictById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddBusinesDict")]
        public async Task<IActionResult> AddBusinesDict([FromBody] Business_Dictionary model)
        {
            try
            {
                var res = await _repo.AddBusinesDict(model);
                return RedirectToAction(nameof(GetBusinesDictById), new { Id = res.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateBusinesDict")]
        public async Task<IActionResult> UpdateBusinesDict([FromBody] Business_Dictionary model)
        {
            try
            {
                return Ok(await _repo.UpdateBusinesDict(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBusinesDict")]
        public async Task<IActionResult> DeleteBusinesDict(Guid Id)
        {
            try
            {
                return Ok(await _repo.DeleteBusinesDict(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
