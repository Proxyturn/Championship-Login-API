using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseProject.Models.Auth.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatchAPI.Controllers
{
    [Route("api/match")]
    public class MatchController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
    }
}

