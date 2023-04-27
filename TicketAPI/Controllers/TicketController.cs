using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketAPI.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketAPI.Controllers
{
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

    }
}

