using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebServer.Dtos;
using WebServer.Interfaces;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _repository;
        public AccountController(IAccount repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(AccountSignInRequestDto request)
        {
            try
            {
                return Ok(await _repository.SignIn(request));
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при авторизации. Неверный логин или пароль");
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] AccountSignUpRequestDto request)
        {
            try
            {
                return Ok(await _repository.SignUp(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
