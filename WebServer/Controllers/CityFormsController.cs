using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityFormsController : ControllerBase
    {
        private readonly ICityForms _repo;
        private readonly IMapper _mapper;
        public CityFormsController(ICityForms repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Документ по селу
        /// </summary>
        /// <param name="katoKod"></param>
        /// <returns>Документ по селу</returns>
        [HttpGet("GetCityDocument")]
        public async Task<ActionResult> GetCityDocument(string katoKod)
        {
            try
            {
                var entity = await _repo.GetCityDocument(katoKod);                
                return Ok(entity);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        /// <summary>
        /// Добавление документа по селу
        /// </summary>
        /// <param name="cityDocumentDto"></param>
        /// <returns></returns>
        [HttpPost("AddCityDocument")]
        [Authorize]
        public async Task<ActionResult> AddCityDocument(CityDocumentDto cityDocumentDto)
        {
            try
            {
                var entity = _mapper.Map<CityDocument>(cityDocumentDto);
                return Ok(await _repo.AddCityDocument(entity)); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение Формы по коду КАТО и году
        /// </summary>
        /// <param name="kodNaselPunk"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("GetCityFormsByKodYear")]
        public async Task<ActionResult> GetCityFormsByKodYear(string kodNaselPunk, int year)
        {
            try
            {
                var entity = await _repo.GetCityFormsByKodYear(kodNaselPunk, year);                
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавление формы 
        /// </summary>
        /// <param name="idDoc"></param>
        /// <param name="cityFormsDto"></param>
        /// <returns></returns>
        [HttpPost("AddCityForms")]
        [Authorize]
        public async Task<ActionResult> AddCityForms(Guid idDoc, CityFormDto cityFormsDto)
        {
            try
            {
                var entity = _mapper.Map<CityForm>(cityFormsDto);
                return Ok(await _repo.AddCityForms(idDoc, entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение Формы села по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSeloFormById")]
        public async Task<ActionResult> GetCityFormById(Guid id)
        {
            try
            {
                var entity = await _repo.GetCityFormById(id);
                if (entity == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<CityFormDto>(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление формы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<CityForm> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var entity = await _repo.GetCityFormById(id);
            if (entity == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(entity, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repo.UpdateCityForm(entity);

            return Ok(entity);
        }

        ///// <summary>
        ///// Получение водоснабжения
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <returns></returns>
        //[HttpGet("GetWaterSupply")]
        //public async Task<ActionResult> GetWaterSupply(Guid idForm)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetWaterSupply(idForm);
        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }                
        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Добавление водоснабжения
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("AddWaterSupply")]
        //[Authorize]
        //public async Task<ActionResult> AddWaterSupply(Guid idForm, CityWaterSupplyDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityWaterSupply>(dto);
        //        return Ok(await _repo.AddWaterSupply(idForm, entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Обновление водоснабжения
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPut("UpdateWaterSupply")]
        //[Authorize]
        //public async Task<ActionResult> UpdateWaterSupply(CityWaterSupplyDto dto)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetWaterSupply(dto.IdForm);
        //        if (entity == null) { return NotFound(); }
        //        var model = _mapper.Map<CityWaterSupply>(dto);
        //        if(model.AutoProccesNasosStanc.HasValue) entity.AutoProccesNasosStanc = model.AutoProccesNasosStanc;
        //        if(model.AutoProccesSetVodosnab.HasValue) entity.AutoProccesSetVodosnab = model.AutoProccesSetVodosnab;


        //        return Ok(await _repo.UpdateWaterSupply(entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}       

        ///// <summary>
        ///// Получение Водоотведения
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <returns></returns>
        //[HttpGet("GetWaterDisposal")]
        //public async Task<ActionResult> GetWaterDisposal(Guid idForm)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetWaterDisposal(idForm);
        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Добавление водоотведения
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("AddWaterDisposal")]
        //[Authorize]
        //public async Task<ActionResult> AddWaterDisposal(Guid idForm, CityWaterDisposalDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityWaterDisposal>(dto);
        //        return Ok(await _repo.AddWaterDisposal(idForm, entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Обновление водоотведения
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPut("UpdateWaterDisposal")]
        //[Authorize]
        //public async Task<ActionResult> UpdateWaterDisposal(CityWaterDisposalDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityWaterDisposal>(dto);
        //        return Ok(await _repo.UpdateWaterDisposal(entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Получение тарифа
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <returns></returns>
        //[HttpGet("GetTarifInfo")]
        //public async Task<ActionResult> GetTarifInfo(Guid idForm)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetTarifInfo(idForm);
        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// Добавление тарифа
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("AddTarifInfo")]
        //[Authorize]
        //public async Task<ActionResult> AddTarifInfo(Guid idForm, CityTarifDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityTarif>(dto);
        //        return Ok(await _repo.AddTarifInfo(idForm, entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Обновление тарифа
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPut("UpdateTariffInfo")]
        //[Authorize]
        //public async Task<ActionResult> UpdateTariffInfo(CityTarifDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityTarif>(dto);
        //        return Ok(await _repo.UpdateTariffInfo(entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Получение Протяженность сетей
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <returns></returns>
        //[HttpGet("GetNetworkLength")]
        //public async Task<ActionResult> GetNetworkLength(Guid idForm)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetNetworkLength(idForm);
        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Добавление Протяженность сетей
        ///// </summary>
        ///// <param name="idForm"></param>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost("AddNetworkLength")]
        //[Authorize]
        //public async Task<ActionResult> AddNetworkLength(Guid idForm, CityNetworkLengthDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityNetworkLength>(dto);
        //        return Ok(await _repo.AddNetworkLength(idForm, entity));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Обновление Протяженность сетей
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPut("UpdateNetworkLength")]
        //[Authorize]
        //public async Task<ActionResult> UpdateNetworkLength(CityNetworkLengthDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<CityNetworkLength>(dto);
        //        return Ok(await _repo.UpdateNetworkLength(entity));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
