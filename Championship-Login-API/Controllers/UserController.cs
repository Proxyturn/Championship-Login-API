using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Championship_Login_API.Models;
using CoreAPI.Business;
using CoreAPI.Repositories;
using CoreAPI.Services;
using DatabaseProject.Models.Auth.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromServices] UserBusiness userBusiness,
                            [FromBody] NewUser newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Body inválido");

            try
            {
                User _user = new User()
                {
                    Id = new Guid(),
                    Name = newUser.Name,
                    Email = newUser.Email,
                    Age = newUser.Age,
                    UserType = newUser.UserType,
                    Password = newUser.Password,
                };

                var insertedUsu = await userBusiness.CadastroDeUserAsync(_user);
                
                return Created($"New User:", insertedUsu);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromServices] TokenService _tokenService,
                                  [FromServices] UserBusiness userBusiness,
                                  [FromBody] LoginUser loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não foi possível validar o objeto");

            try
            {
                
                var usuConsulta = await userBusiness.VerificarUsuarioSenhaAsync(loginUser);

                if (usuConsulta != null)
                {
                    
                    var token = await _tokenService.GenerateToken(usuConsulta);
                    return Ok(
                        new
                        {
                            status = HttpStatusCode.OK,
                            Token = token
                        });
                }
                else
                {
                    return NotFound(
                        new
                        {
                            status = HttpStatusCode.NotFound,
                            Error = "Usuário não encontrado"
                        });
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.CompareTo("NotCompatible") == 0)
                    return Unauthorized(new
                    {
                        status = HttpStatusCode.Unauthorized,
                        Error = "Senha não compatível ao usuário"
                    });

                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromServices] UserBusiness userBusiness, Guid id)
        {
            try
            {
                var user = await userBusiness.GetUserByIdAsync(id);

                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromServices] UserBusiness userBusiness, Guid id)
        {
            try
            {
                var sucess = await userBusiness.DeleteUserAsync(id);

                if (!sucess)
                {
                    return NotFound(
                       new
                       {
                           status = HttpStatusCode.NotFound,
                           Error = "Não foi possível deletar usuário informado"
                       });
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromServices] UserBusiness userBusiness, UpdateUser updateUser)
        {
            try
            {
                var updatedUser = await userBusiness.UpdateUserAsync(updateUser);

                return StatusCode(200, updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

