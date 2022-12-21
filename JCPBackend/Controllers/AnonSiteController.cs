using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonSiteController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public AnonSiteController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/AnonSite
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<j_site>>> Getj_sites()
        {
            return await _context.j_sites.ToListAsync();
        }
    }
}
