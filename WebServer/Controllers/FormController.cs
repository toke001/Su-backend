using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.Dtos;
using WebServer.Helpers;
using WebServer.Interfaces;
using WebServer.Models;
using WebServer.Reposotory;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FormController : ControllerBase
    {
        private readonly IForms _repo;

        public FormController(IForms repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetForms()
        {
            try
            {
                return Ok(await _repo.GetForms());
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ApprovedForm aForm)
        {
            try
            {
                return Ok(await _repo.Add(aForm));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApprovedForm aForm)
        {
            try
            {
                return Ok(await _repo.Update(aForm));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                return Ok(await _repo.Delete(id));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при выполнении запроса");
            }
        }

        //[HttpGet("list")]
        //public async Task<IActionResult> GetForms([FromQuery] int katoid)
        //{
        //    try
        //    {
        //        return Ok(await _repository.GetFormsByKatoId(katoid));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("add")]
        //public async Task<IActionResult> AddForm([FromBody] FormsAddDto dto)
        //{
        //    try
        //    {
        //        return Ok(await _repository.AddForm(dto));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //#region form 1 водоснабжение
        //[HttpGet("supply/city/form1/list")]
        //public async Task<IActionResult> GetForm1ById([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityGetForm1(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("supply/city/form1/list/update")]
        //public async Task<IActionResult> UpdateForm1ById([FromBody] List<SupplyCityForm1TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityUpdateForm1(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion

        //#region form 2 водоснабжение
        //[HttpGet("supply/city/form2/list")]
        //public async Task<IActionResult> GetForm2ById([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityGetForm2(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("supply/city/form2/list/update")]
        //public async Task<IActionResult> UpdateForm2ById([FromBody] List<SupplyCityForm2TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityUpdateForm2(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion

        //#region form 3 водоснабжение
        //[HttpGet("supply/city/form3/list")]
        //public async Task<IActionResult> GetForm3ById([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityGetForm3(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("supply/city/form3/list/update")]
        //public async Task<IActionResult> UpdateForm3ById([FromBody] List<SupplyCityForm3TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityUpdateForm3(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion

        //#region form 4 водоснабжение
        //[HttpGet("supply/city/form4/list")]
        //public async Task<IActionResult> GetForm4ById([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityGetForm4(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("supply/city/form4/list/update")]
        //public async Task<IActionResult> UpdateForm4ById([FromBody] List<SupplyCityForm4TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityUpdateForm4(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion

        //#region form 5 водоснабжение
        //[HttpGet("supply/city/form5/list")]
        //public async Task<IActionResult> GetForm5ById([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityGetForm5(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("supply/city/form5/list/update")]
        //public async Task<IActionResult> UpdateForm5ById([FromBody] List<SupplyCityForm5TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.SupplyCityUpdateForm5(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion
        //#region form 1 водоотведение город
        //[HttpGet("waste/city/form1/list")]
        //public async Task<IActionResult> WasteCityGetForm1([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.WasteCityGetForm1(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("waste/city/form1/list/update")]
        //public async Task<IActionResult> WasteCityUpdateForm1([FromBody] List<WasteCityForm1TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.WasteCityUpdateForm1(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion
        //#region form 2 водоотведение город
        //[HttpGet("waste/city/form2/list")]
        //public async Task<IActionResult> WasteCityGetForm2([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.WasteCityGetForm2(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("waste/city/form2/list/update")]
        //public async Task<IActionResult> WasteCityUpdateForm2([FromBody] List<WasteCityForm2TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.WasteCityUpdateForm2(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion
        //#region form 3 водоотведение город
        //[HttpGet("waste/city/form3/list")]
        //public async Task<IActionResult> WasteCityGetForm3([FromQuery] Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.WasteCityGetForm3(id));
        //        //HttpContext.FromQuery(),
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("waste/city/form3/list/update")]
        //public async Task<IActionResult> WasteCityUpdateForm3([FromBody] List<WasteCityForm3TableDto> list, Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _repository.WasteCityUpdateForm3(list, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //#endregion

    }
}
