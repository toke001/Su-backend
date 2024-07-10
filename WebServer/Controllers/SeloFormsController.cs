using Humanizer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeloFormsController : ControllerBase
    {
        private readonly ISeloForms _repo;
        public SeloFormsController(ISeloForms repo)
        {
            _repo = repo;
        }


        [HttpGet("GetSeloDocument")]
        public async Task<ActionResult> GetSeloDocument(string katoKod)
        {
            try
            {
                var entity = await _repo.GetSeloDocument(katoKod);
                if (entity == null)
                {
                    return NotFound();
                }
                //var dto = _mapper.Map<YourDto>(entity);
                return Ok(entity);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost("AddSeloDocument")]
        public async Task<ActionResult> AddSeloDocument(SeloDocument seloDoument)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.AddSeloDocument(seloDoument)); 
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSeloFormsByKodYear")]
        public async Task<ActionResult> GetSeloFormsByKodYear(string kodNaselPunk, int year)
        {
            try
            {
                var entity = await _repo.GetSeloFormsByKodYear(kodNaselPunk, year);
                if (entity == null)
                {
                    return NotFound();
                }
                //var dto = _mapper.Map<YourDto>(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSeloForms")]
        public async Task<ActionResult> AddSeloForms(Guid idDoc, SeloForms seloForms)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.AddSeloForms(idDoc, seloForms));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWaterSupply")]
        public async Task<ActionResult> GetWaterSupply(Guid idForm)
        {
            try
            {
                var entity = await _repo.GetWaterSupply(idForm);
                if (entity == null)
                {
                    return NotFound();
                }
                //var dto = _mapper.Map<YourDto>(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddWaterSupply")]
        public async Task<ActionResult> AddWaterSupply(Guid idForm, WaterSupplyInfo waterSupplyInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.AddWaterSupply(idForm, waterSupplyInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateWaterSupply")]
        public async Task<ActionResult> UpdateWaterSupply(WaterSupplyInfo waterSupplyInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.UpdateWaterSupply(waterSupplyInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWaterDisposal")]
        public async Task<ActionResult> GetWaterDisposal(Guid idForm)
        {
            try
            {
                var entity = await _repo.GetWaterDisposal(idForm);
                if (entity == null)
                {
                    return NotFound();
                }
                //var dto = _mapper.Map<YourDto>(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddWaterDisposal")]
        public async Task<ActionResult> AddWaterDisposal(Guid idForm, WaterDisposalInfo waterDisposalInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.AddWaterDisposal(idForm, waterDisposalInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateWaterDisposal")]
        public async Task<ActionResult> UpdateWaterDisposal(WaterDisposalInfo waterDisposalInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.UpdateWaterDisposal(waterDisposalInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTarifInfo")]
        public async Task<ActionResult> GetTarifInfo(Guid idForm)
        {
            try
            {
                var entity = await _repo.GetTarifInfo(idForm);
                if (entity == null)
                {
                    return NotFound();
                }
                //var dto = _mapper.Map<YourDto>(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddTarifInfo")]
        public async Task<ActionResult> AddTarifInfo(Guid idForm, TariffInfo tariffInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.AddTarifInfo(idForm, tariffInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateTariffInfo")]
        public async Task<ActionResult> UpdateTariffInfo(TariffInfo tariffInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.UpdateTariffInfo(tariffInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetNetworkLength")]
        public async Task<ActionResult> GetNetworkLength(Guid idForm)
        {
            try
            {
                var entity = await _repo.GetNetworkLength(idForm);
                if (entity == null)
                {
                    return NotFound();
                }
                //var dto = _mapper.Map<YourDto>(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNetworkLength")]
        public async Task<ActionResult> AddNetworkLength(Guid idForm, NetworkLengthInfo networkLengthInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.AddNetworkLength(idForm, networkLengthInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateNetworkLength")]
        public async Task<ActionResult> UpdateNetworkLength(NetworkLengthInfo networkLengthInfo)
        {
            try
            {
                //var entity = _mapper.Map<YourEntity>(dto);
                return Ok(await _repo.UpdateNetworkLength(networkLengthInfo));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
