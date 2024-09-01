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
        /// Список документов по коду области, района и по году
        /// </summary>
        /// <param name="kodOblast"></param>
        /// <param name="kodRaion"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("GetCityDocumentByParams")]
        public async Task<ActionResult> GetCityDocumentByParams(string? kodOblast, string? kodRaion, int? year)
        {
            try
            {
                var entity = await _repo.GetCityDocumentByParams(kodOblast, kodRaion, year);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Все формы по id документа
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        [HttpGet("GetCityFormsByDocId")]
        public async Task<ActionResult> GetCityFormsByDocId(Guid idDoc)
        {
            try
            {
                var entity = await _repo.GetCityFormsByDocId(idDoc);
                return Ok(entity);
            }
            catch (Exception ex)
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
        /// <param name="login"></param>
        /// <param name="cityFormDto"></param>
        /// <returns></returns>
        [HttpPost("AddCityForms")]
        [Authorize]
        public async Task<ActionResult> AddCityForms(string login, List<CityFormDto> cityFormDto)
        {
            try
            {
                var entity = _mapper.Map<List<CityForm>>(cityFormDto);
                return Ok(await _repo.AddCityForms(login, entity));
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
        [HttpGet("GetCityFormById")]
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
        /// <param name="login"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(Guid id, string login, [FromBody] JsonPatchDocument<CityForm> patchDoc)
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

            await _repo.UpdateCityForm(login, entity);

            return Ok(entity);
        }        
    }
}
