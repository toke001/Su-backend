using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeloFormsController : ControllerBase
    {
        private readonly ISeloForms _repo;
        private readonly IMapper _mapper;
        public SeloFormsController(ISeloForms repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Документ по селу
        /// </summary>
        /// <param name="katoKod"></param>
        /// <returns>Документ по селу</returns>
        [HttpGet("GetSeloDocument")]
        public async Task<ActionResult> GetSeloDocument(string katoKod)
        {
            try
            {
                var entity = await _repo.GetSeloDocument(katoKod);
                return Ok(entity);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        /// <summary>
        /// Добавление документа по селу
        /// </summary>
        /// <param name="seloDocumentDto"></param>
        /// <returns></returns>
        [HttpPost("AddSeloDocument")]
        [Authorize]
        public async Task<ActionResult> AddSeloDocument(SeloDocumentDto seloDocumentDto)
        {
            try
            {
                var entity = _mapper.Map<SeloDocument>(seloDocumentDto);
                var dto = _mapper.Map<SeloDocumentDto>(await _repo.AddSeloDocument(entity));
                return Ok(dto); 
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
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
        [HttpGet("GetSeloFormsByKodYear")]
        public async Task<ActionResult> GetSeloFormsByKodYear(string kodNaselPunk, int year)
        {
            try
            {
                var entity = await _repo.GetSeloFormsByKodYear(kodNaselPunk, year);    
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
        /// <param name="seloFormDto"></param>
        /// <returns></returns>
        [HttpPost("AddSeloForms")]
        [Authorize]
        public async Task<ActionResult> AddSeloForms(Guid idDoc, SeloFormDto seloFormDto)
        {
            try
            {
                var entity = _mapper.Map<SeloForm>(seloFormDto);
                return Ok(await _repo.AddSeloForms(idDoc, entity));
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
        public async Task<ActionResult> GetSeloFormById(Guid id)
        {
            try
            {
                var entity = await _repo.GetSeloFormById(id);
                if (entity == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<SeloFormDto>(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<SeloForm> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var entity = await _repo.GetSeloFormById(id);
            if (entity == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(entity, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repo.UpdateSeloForm(entity);

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
        //        var dto = _mapper.Map<SeloWaterSupplyDto>(entity);
        //        return Ok(dto);
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
        //public async Task<ActionResult> AddWaterSupply(Guid idForm, SeloWaterSupplyDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<SeloWaterSupply>(dto);
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
        //public async Task<ActionResult> UpdateWaterSupply(SeloWaterSupplyDto dto)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetWaterSupply(dto.IdForm);
        //        if (entity == null) { return NotFound(); }
        //        var model = _mapper.Map<SeloWaterSupply>(dto);
        //        if(model.CentrVodoIndivPriborUchVodyASYE.HasValue) entity.CentrVodoIndivPriborUchVodyASYE = model.CentrVodoIndivPriborUchVodyASYE;
        //        if(model.CentrVodoIndivPriborUchVodyOhvat.HasValue) entity.CentrVodoIndivPriborUchVodyOhvat = model.CentrVodoIndivPriborUchVodyOhvat;
        //        if(model.CentrVodoIndivPriborUchVodyVsego.HasValue) entity.CentrVodoIndivPriborUchVodyVsego = model.CentrVodoIndivPriborUchVodyVsego;
        //        if(model.CentrVodoSnabBudzhOrg.HasValue) entity.CentrVodoSnabBudzhOrg = model.CentrVodoSnabBudzhOrg;
        //        if(model.CentrVodoSnabFizLic.HasValue) entity.CentrVodoSnabFizLic = model.CentrVodoSnabFizLic;
        //        if(model.CentrVodoSnabKolAbon.HasValue) entity.CentrVodoSnabKolAbon = model.CentrVodoSnabKolAbon;
        //        if(model.CentrVodoSnabKolChel.HasValue) entity.CentrVodoSnabKolChel = model.CentrVodoSnabKolChel;
        //        if(model.CentrVodoSnabKolNasPun.HasValue) entity.CentrVodoSnabKolNasPun = model.CentrVodoSnabKolNasPun;
        //        if(model.CentrVodoSnabObesKolChel.HasValue) entity.CentrVodoSnabObesKolChel = model.CentrVodoSnabObesKolChel;
        //        if(model.CentrVodoSnabObesKolNasPunk.HasValue) entity.CentrVodoSnabObesKolNasPunk = model.CentrVodoSnabObesKolNasPunk;
        //        if(model.CentrVodoSnabYriLic.HasValue) entity.CentrVodoSnabYriLic = model.CentrVodoSnabYriLic;
        //        if(model.DosVodoSnabKolChel.HasValue) entity.DosVodoSnabKolChel = model.DosVodoSnabKolChel;
        //        if(model.DosVodoSnabKolPunk.HasValue) entity.DosVodoSnabKolPunk = model.DosVodoSnabKolPunk;
        //        if(model.DosVodoSnabPercent.HasValue) entity.DosVodoSnabPercent = model.DosVodoSnabPercent;
        //        if(model.KbmKolChel.HasValue) entity.KbmKolChel = model.KbmKolChel;
        //        if(model.KbmKolSelsNasPunk.HasValue) entity.KbmKolSelsNasPunk = model.KbmKolSelsNasPunk;
        //        if(model.KbmObespNasel.HasValue) entity.KbmObespNasel = model.KbmObespNasel;
        //        if(model.NeCtentrVodoKolSelsNasPunk.HasValue) entity.NeCtentrVodoKolSelsNasPunk = model.NeCtentrVodoKolSelsNasPunk;
        //        if(model.PrivVodaKolChel.HasValue) entity.PrivVodaKolChel = model.PrivVodaKolChel;
        //        if(model.PrivVodaKolSelsNasPunk.HasValue) entity.PrivVodaKolSelsNasPunk = model.PrivVodaKolSelsNasPunk;
        //        if(model.PrivVodaObespNasel.HasValue) entity.PrivVodaObespNasel = model.PrivVodaObespNasel;
        //        if(model.PrvKolChel.HasValue) entity.PrvKolChel = model.PrvKolChel;
        //        if(model.PrvKolSelsNasPunk.HasValue) entity.PrvKolSelsNasPunk = model.PrvKolSelsNasPunk;
        //        if(model.PrvObespNasel.HasValue) entity.PrvObespNasel = model.PrvObespNasel;
        //        if(model.SkvazhDolyaNaselOtkaz.HasValue) entity.SkvazhDolyaNaselOtkaz = model.SkvazhDolyaNaselOtkaz;
        //        if(model.SkvazhDolyaSelOtkaz.HasValue) entity.SkvazhDolyaSelOtkaz = model.SkvazhDolyaSelOtkaz;
        //        if(model.SkvazhKolChel.HasValue) entity.SkvazhKolChel = model.SkvazhKolChel;
        //        if(model.SkvazhKolChelOtkaz.HasValue) entity.SkvazhKolChelOtkaz = model.SkvazhKolChelOtkaz;
        //        if(model.SkvazhKolSelsNasPunk.HasValue) entity.SkvazhKolSelsNasPunk = model.SkvazhKolSelsNasPunk;
        //        if(model.SkvazhKolSelsNasPunkOtkaz.HasValue) entity.SkvazhKolSelsNasPunkOtkaz = model.SkvazhKolSelsNasPunkOtkaz;
        //        if(model.SkvazhObespNasel.HasValue) entity.SkvazhObespNasel = model.SkvazhObespNasel;

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
        //        var dto = _mapper.Map<SeloWaterDisposalDto>(entity);
        //        return Ok(dto);
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
        //public async Task<ActionResult> AddWaterDisposal(Guid idForm, SeloWaterDisposalDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<SeloWaterDisposal>(dto);
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
        //public async Task<ActionResult> UpdateWaterDisposal(SeloWaterDisposalDto dto)
        //{
        //    try
        //    {
        //        var entity = await _repo.GetWaterDisposal(dto.IdForm);
        //        if (entity == null) { return NotFound(); }
        //        var model = _mapper.Map<SeloWaterDisposal>(dto);
        //        if (model.CentrVodOtvedBydzhOrg.HasValue) entity.CentrVodOtvedBydzhOrg = model.CentrVodOtvedBydzhOrg;
        //        if (model.CentrVodOtvedDostypKolChel.HasValue) entity.CentrVodOtvedDostypKolChel = model.CentrVodOtvedDostypKolChel;
        //        if (model.CentrVodOtvedDostypKolNasPunk.HasValue) entity.CentrVodOtvedDostypKolNasPunk = model.CentrVodOtvedDostypKolNasPunk;
        //        if (model.CentrVodOtvedFactPostypStochVod.HasValue) entity.CentrVodOtvedFactPostypStochVod = model.CentrVodOtvedFactPostypStochVod;
        //        if (model.CentrVodOtvedFactPostypStochVod1.HasValue) entity.CentrVodOtvedFactPostypStochVod1 = model.CentrVodOtvedFactPostypStochVod1;
        //        if (model.CentrVodOtvedFactPostypStochVod2.HasValue) entity.CentrVodOtvedFactPostypStochVod2 = model.CentrVodOtvedFactPostypStochVod2;
        //        if (model.CentrVodOtvedFactPostypStochVod3.HasValue) entity.CentrVodOtvedFactPostypStochVod3 = model.CentrVodOtvedFactPostypStochVod3;
        //        if (model.CentrVodOtvedFactPostypStochVod4.HasValue) entity.CentrVodOtvedFactPostypStochVod4 = model.CentrVodOtvedFactPostypStochVod4;
        //        if (model.CentrVodOtvedFizLic.HasValue) entity.CentrVodOtvedFizLic = model.CentrVodOtvedFizLic;
        //        if (model.CentrVodOtvedIznos.HasValue) entity.CentrVodOtvedIznos = model.CentrVodOtvedIznos;
        //        if (model.CentrVodOtvedKolAbonent.HasValue) entity.CentrVodOtvedKolAbonent = model.CentrVodOtvedKolAbonent;
        //        if (model.CentrVodOtvedKolChel.HasValue) entity.CentrVodOtvedKolChel = model.CentrVodOtvedKolChel;
        //        if (model.CentrVodOtvedKolSelsNasPunk.HasValue) entity.CentrVodOtvedKolSelsNasPunk = model.CentrVodOtvedKolSelsNasPunk;
        //        if (model.CentrVodOtvedNalich.HasValue) entity.CentrVodOtvedNalich = model.CentrVodOtvedNalich;
        //        if (model.CentrVodOtvedNalichMechan.HasValue) entity.CentrVodOtvedNalichMechan = model.CentrVodOtvedNalichMechan;
        //        if (model.CentrVodOtvedNalichMechanBiolog.HasValue) entity.CentrVodOtvedNalichMechanBiolog = model.CentrVodOtvedNalichMechanBiolog;
        //        if (model.CentrVodOtvedObiemStochVod.HasValue) entity.CentrVodOtvedObiemStochVod = model.CentrVodOtvedObiemStochVod;
        //        if (model.CentrVodOtvedOhvatKolChel.HasValue) entity.CentrVodOtvedOhvatKolChel = model.CentrVodOtvedOhvatKolChel;
        //        if (model.CentrVodOtvedOhvatNasel.HasValue) entity.CentrVodOtvedOhvatNasel = model.CentrVodOtvedOhvatNasel;
        //        if (model.CentrVodOtvedProizvod.HasValue) entity.CentrVodOtvedProizvod = model.CentrVodOtvedProizvod;
        //        if (model.CentrVodOtvedUrovenNorm.HasValue) entity.CentrVodOtvedUrovenNorm = model.CentrVodOtvedUrovenNorm;
        //        if (model.CentrVodOtvedYriLic.HasValue) entity.CentrVodOtvedYriLic = model.CentrVodOtvedYriLic;
        //        if (model.DecentrVodoOtvedKolChel.HasValue) entity.DecentrVodoOtvedKolChel = model.DecentrVodoOtvedKolChel;
        //        if (model.DecentrVodoOtvedKolSelsNasPunk.HasValue) entity.DecentrVodoOtvedKolSelsNasPunk = model.DecentrVodoOtvedKolSelsNasPunk;                

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
        //        //var dto = _mapper.Map<YourDto>(entity);
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
        //public async Task<ActionResult> AddTarifInfo(Guid idForm, SeloTariffDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<SeloTariff>(dto);
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
        //public async Task<ActionResult> UpdateTariffInfo(SeloTariffDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<SeloTariff>(dto);
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
        //        //var dto = _mapper.Map<YourDto>(entity);
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
        //public async Task<ActionResult> AddNetworkLength(Guid idForm, SeloNetworkLengthDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<SeloNetworkLength>(dto);
        //        return Ok(await _repo.AddNetworkLength(idForm, entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
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
        //public async Task<ActionResult> UpdateNetworkLength(SeloNetworkLengthDto dto)
        //{
        //    try
        //    {
        //        var entity = _mapper.Map<SeloNetworkLength>(dto);
        //        return Ok(await _repo.UpdateNetworkLength(entity));
        //        //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
