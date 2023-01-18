using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class AccountsController : ControllerBase
    {
        public readonly IAuthService _authservice;

        public AccountsController(IAuthService service)
        {
            _authservice = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                await _authservice.RegisterAsync(registerDTO);
                return Ok("User Succesfully created");
            }
            catch (UserCreateFailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RoleCreateFailException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var responseToken = await _authservice.LoginAsync(loginDTO);
                return Ok(responseToken);
            }
            catch (AuthCreateFailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)(HttpStatusCode.InternalServerError));
            }
        }
    }
}
