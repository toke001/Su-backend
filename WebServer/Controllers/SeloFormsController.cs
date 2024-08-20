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
        /// Список документов по коду области, района и по году
        /// </summary>
        /// <param name="kodOblast"></param>
        /// <param name="kodRaion"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("GetSeloDocumentByParams")]
        public async Task<ActionResult> GetSeloDocumentByParams(string? kodOblast, string? kodRaion, int? year)
        {
            try
            {
                var entity = await _repo.GetSeloDocumentByParams(kodOblast, kodRaion, year);
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
        [HttpGet("GetSeloFormsByDocId")]
        public async Task<ActionResult> GetSeloFormsByDocId(Guid idDoc)
        {
            try
            {
                var entity = await _repo.GetSeloFormsByDocId(idDoc);
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

        [HttpGet("FindParentRecordAsync")]
        public async Task<ActionResult> FindParentRecordAsync(int parentId, int katoLevel)
        {
            try
            {
                return Ok(await _repo.FindParentRecordAsync(parentId, katoLevel));
            }
            catch (Exception ex)
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
        public async Task<ActionResult> AddSeloForms(string login, List<SeloFormDto> seloFormDto)
        {
            try
            {
                var entity = _mapper.Map<List<SeloForm>>(seloFormDto);
                return Ok(await _repo.AddSeloForms(login, entity));
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
        /// Обновление формы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(Guid id, string login, [FromBody] JsonPatchDocument<SeloForm> patchDoc)
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

            await _repo.UpdateSeloForm(login, entity);

            return Ok(entity);
        }

        
    }
}
