using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.Data;
using Tesis.Entities;

namespace Tesis.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secretText";
        }
        
        
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var notFounrException = _context.Users.Find(-1);

            if (notFounrException == null)
                return NotFound();

            return Ok(notFounrException); 
        }
        
        
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {

            var thing = _context.Users.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;

        }
        
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

    }
}
