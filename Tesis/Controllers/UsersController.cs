using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesis.Entities;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userRepo.GetUsersAsync());
        }

        //[Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            return Ok(await _userRepo.GetUserByUsernameAsync(username));
        }
    }
}
