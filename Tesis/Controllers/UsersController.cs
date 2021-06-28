using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDto>>> GetUsers()
        {
            //    var users = await _userRepo.GetUsersAsync();
            //    var usersToReturn = _mapper.Map<IEnumerable<AgentDto>>(users);

            //    return Ok(usersToReturn);
            return Ok(await _userRepo.GetAgentsAsync());
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<AgentDto>> GetUser(string username)
        {
            //var user = await _userRepo.GetUserByUsernameAsync(username);
            //var userToReturn = _mapper.Map<AgentDto>(user);

            //return Ok(userToReturn);

            return await _userRepo.GetAgentByUsernameAsync(username);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Updateuser(AgentUpdateDto agentUpdateDto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepo.GetUserByUsernameAsync(userName);

            _mapper.Map(agentUpdateDto, user);

            _userRepo.Update(user);

            if (await _userRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Fail to update user");
        }
    }
}
