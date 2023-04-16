using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Championship_Login_API.Models;
using ChampionshipAPI.Business;
using DatabaseProject.Models.Auth.Request;
using DatabaseProject.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChampionshipAPI.Controllers
{
    [Route("api/championship")]
    public class ChampionshipController : Controller
    {
        private ChampionshipBusiness _championshipBusiness;
        public ChampionshipController(ChampionshipBusiness championshipBusiness)
        {
            _championshipBusiness = championshipBusiness;
        }

        /// <summary>
        /// GetAll the Championships in internal level with Auth
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/internal")]
        public async Task<IActionResult> GetAllInternal()
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.GetAll(false));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetAll the Championships in external level no Auth
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/external")]
        public async Task<IActionResult> GetAllExternal()
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get championship info in internal level with Auth
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/internal/{id}")]
        public async Task<IActionResult> GetByIdInternal(Guid id)
        {
            try
            {
                
                return StatusCode(200, await _championshipBusiness.GetById(id, false));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get championship info in external level no Auth
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/external/{id}")]
        public async Task<IActionResult> GetByIdExternal(Guid id)
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Create new Championship
        /// </summary>
        /// <param name="value"></param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChampionship createChampionship)
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.Insert(createChampionship));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Start a championship, generate all matchs
        /// </summary>
        /// <param name="createChampionship"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("/startChampionship")]
        public async Task<IActionResult> StartChampionship([FromBody] Guid IdChampionship)
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.StartChampionship(IdChampionship));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// End championship, generate top ranking
        /// </summary>
        /// <param name="createChampionship"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("/finishChampionship")]
        public async Task<IActionResult> FinishChampionship([FromBody] Guid IdChampionship)
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.FinishChampionship(IdChampionship));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update existing championship
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Championship championship)
        {
            try
            {
                return StatusCode(201, await _championshipBusiness.Update(championship));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an specific championship
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return StatusCode(200, await _championshipBusiness.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

