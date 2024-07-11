using AutoMapper;
using Humanizer;
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
        public async Task<ActionResult<List<SeloDocument>>> GetSeloDocument(string katoKod)
        {
            try
            {
                var entity = await _repo.GetSeloDocument(katoKod);
                if (entity == null)
                {
                    return NotFound();
                }
                var dtos = _mapper.Map<IEnumerable<SeloDocument>>(entity);
                return Ok(dtos);
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
                if (entity == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<SeloFormsDto>(entity);
                return Ok(dto);
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
        /// <param name="seloFormsDto"></param>
        /// <returns></returns>
        [HttpPost("AddSeloForms")]
        public async Task<ActionResult> AddSeloForms(Guid idDoc, SeloFormsDto seloFormsDto)
        {
            try
            {
                var entity = _mapper.Map<SeloForms>(seloFormsDto);
                return Ok(await _repo.AddSeloForms(idDoc, entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение водоснабжения
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
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
                var dto = _mapper.Map<WaterSupplyInfoDto>(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавление водоснабжения
        /// </summary>
        /// <param name="idForm"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("AddWaterSupply")]
        public async Task<ActionResult> AddWaterSupply(Guid idForm, WaterSupplyInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<WaterSupplyInfo>(dto);
                return Ok(await _repo.AddWaterSupply(idForm, entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление водоснабжения
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UpdateWaterSupply")]
        public async Task<ActionResult> UpdateWaterSupply(WaterSupplyInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<WaterSupplyInfo>(dto);
                return Ok(await _repo.UpdateWaterSupply(entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение Водоотведения
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
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
                var dto = _mapper.Map<WaterDisposalInfoDto>(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавление водоотведения
        /// </summary>
        /// <param name="idForm"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("AddWaterDisposal")]
        public async Task<ActionResult> AddWaterDisposal(Guid idForm, WaterDisposalInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<WaterDisposalInfo>(dto);
                return Ok(await _repo.AddWaterDisposal(idForm, entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление водоотведения
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UpdateWaterDisposal")]
        public async Task<ActionResult> UpdateWaterDisposal(WaterDisposalInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<WaterDisposalInfo>(dto);
                return Ok(await _repo.UpdateWaterDisposal(entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение тарифа
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Добавление тарифа
        /// </summary>
        /// <param name="idForm"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("AddTarifInfo")]
        public async Task<ActionResult> AddTarifInfo(Guid idForm, TariffInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<TariffInfo>(dto);
                return Ok(await _repo.AddTarifInfo(idForm, entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление тарифа
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UpdateTariffInfo")]
        public async Task<ActionResult> UpdateTariffInfo(TariffInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<TariffInfo>(dto);
                return Ok(await _repo.UpdateTariffInfo(entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение Протяженность сетей
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Добавление Протяженность сетей
        /// </summary>
        /// <param name="idForm"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("AddNetworkLength")]
        public async Task<ActionResult> AddNetworkLength(Guid idForm, NetworkLengthInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<NetworkLengthInfo>(dto);
                return Ok(await _repo.AddNetworkLength(idForm, entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление Протяженность сетей
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UpdateNetworkLength")]
        public async Task<ActionResult> UpdateNetworkLength(NetworkLengthInfoDto dto)
        {
            try
            {
                var entity = _mapper.Map<NetworkLengthInfo>(dto);
                return Ok(await _repo.UpdateNetworkLength(entity));
                //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
