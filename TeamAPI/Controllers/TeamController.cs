using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAPI.Business;

namespace TeamAPI.Controllers
{
    [Authorize]
    [Route("api/team")]
    public class TeamController : Controller
    {
        private readonly TeamBusiness _teamBusiness;

        public TeamController(TeamBusiness teamBusiness)
        {
            _teamBusiness = teamBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTeam()
        {
            try
            {
                var teamComponents = _teamBusiness.GetUserTeam();
                return Ok(teamComponents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

