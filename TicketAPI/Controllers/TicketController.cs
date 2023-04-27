﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseProject.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketAPI.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketAPI.Controllers
{
    [Authorize]
    [Route("api/ticket")]
    public class TicketController : Controller
    {
        private TicketBusiness _ticketBusiness;
        public TicketController(TicketBusiness ticketBusiness)
        {
            _ticketBusiness = ticketBusiness;
        }
        
        [HttpGet("/getAll/available")]
        public async Task<IActionResult> GetAllAvailable()
        {
            try
            {
                return StatusCode(200, await _ticketBusiness.GetAllAvailable());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Guid matchId)
        {
            try
            {
                string userId = (HttpContext.User.Claims.SingleOrDefault(p => p.Type == "userId"))?.Value;
                return StatusCode(201, await _ticketBusiness.Insert(matchId, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

