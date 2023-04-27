using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Championship_Login_API.Models;
using CoreAPI.Business;
using DatabaseProject.Models.Auth.Request;
using DatabaseProject.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Authorize]
    [Route("api/referee")]
    public class RefereeController : Controller
    {
        private RefereeBusiness _refereeBusiness;
        public RefereeController(RefereeBusiness refereeBusiness)
        {
            _refereeBusiness = refereeBusiness;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return StatusCode(201, await _refereeBusiness.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return StatusCode(201, await _refereeBusiness.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUser newUser)
        {
            try
            {
                return StatusCode(201, await _refereeBusiness.Insert(newUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/registerChampionship")]
        public async Task<IActionResult> RegisterChampionship([FromBody] RegisterRefereeChampionship registerRefereeChampionship)
        {
            try
            {
                return StatusCode(201, await _refereeBusiness.RegisterChampionship(registerRefereeChampionship));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateUser updateUser)
        {
            try
            {
                return StatusCode(201, await _refereeBusiness.Update(updateUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return StatusCode(201, await _refereeBusiness.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

