using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JCPBackend.Models;
using System.Net;

namespace JCPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteStatusController : ControllerBase
    {
        private readonly JobCostProTestingContext _context;

        public QuoteStatusController(JobCostProTestingContext context)
        {
            _context = context;
        }

        // GET: api/QuoteStatus
        [HttpGet]
        public IEnumerable<string> Getj_quote_status()
        {
            return _context.j_quote_statuses.OrderBy(u => u.sort_order).Select(u => u.status);
        }

        // PUT: api/QuoteStatus
        [HttpPut]
        public IResult PutQuoteStatus(j_quote_status[] j_quote_status)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            _context.j_quote_statuses.RemoveRange(_context.j_quote_statuses.ToList());

            var i = 0;
            foreach (var q in j_quote_status)
            {
                q.sort_order = i++;
            }

            _context.j_quote_statuses.AddRange(j_quote_status);

            _context.SaveChanges();

            return Results.NoContent();
        }

        private bool JQuoteStatusExists(string id)
        {
            return _context.j_quote_statuses.Any(e => e.status == id);
        }
    }
}
