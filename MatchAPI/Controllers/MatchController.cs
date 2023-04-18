using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseProject.Models.Auth.Request;
using MatchAPI.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatchAPI.Controllers
{
    [Route("api/match")]
    public class MatchController : Controller
    {
        private MatchBusiness _matchBusiness;
        public MatchController(MatchBusiness matchBusiness)
        {
            _matchBusiness = matchBusiness;
        }

        [HttpGet("/getByRefereeId/{IdReferee}")]
        public async Task<IActionResult> GetByRefereeId(Guid IdReferee)
        {
            try
            {
                return StatusCode(200, await _matchBusiness.GetByRefereeId(IdReferee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/getMatchById/{IdMatch}")]
        public async Task<IActionResult> GetMatchId(Guid IdMatch)
        {
            try
            {
                return StatusCode(200, await _matchBusiness.GetMatchById(IdMatch));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

