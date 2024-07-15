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
        public async Task<ActionResult<List<CityDocument>>> GetCityDocument(string katoKod)
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
        public async Task<ActionResult> AddCityForms(Guid idDoc, CityFormsDto cityFormsDto)
        {
            try
            {
                var entity = _mapper.Map<CityForms>(cityFormsDto);
                return Ok(await _repo.AddCityForms(idDoc, entity));
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
                return Ok(entity);
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
        public async Task<ActionResult> AddWaterSupply(Guid idForm, CityWaterSupplyDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityWaterSupply>(dto);
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
        [HttpPut("UpdateWaterSupply")]
        public async Task<ActionResult> UpdateWaterSupply(CityWaterSupplyDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityWaterSupply>(dto);
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
                return Ok(entity);
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
        public async Task<ActionResult> AddWaterDisposal(Guid idForm, CityWaterDisposalDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityWaterDisposal>(dto);
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
        [HttpPut("UpdateWaterDisposal")]
        public async Task<ActionResult> UpdateWaterDisposal(CityWaterDisposalDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityWaterDisposal>(dto);
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
        public async Task<ActionResult> AddTarifInfo(Guid idForm, CityTarifDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityTarif>(dto);
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
        [HttpPut("UpdateTariffInfo")]
        public async Task<ActionResult> UpdateTariffInfo(CityTarifDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityTarif>(dto);
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
        public async Task<ActionResult> AddNetworkLength(Guid idForm, CityNetworkLengthDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityNetworkLength>(dto);
                return Ok(await _repo.AddNetworkLength(idForm, entity));
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
        [HttpPut("UpdateNetworkLength")]
        public async Task<ActionResult> UpdateNetworkLength(CityNetworkLengthDto dto)
        {
            try
            {
                var entity = _mapper.Map<CityNetworkLength>(dto);
                return Ok(await _repo.UpdateNetworkLength(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
