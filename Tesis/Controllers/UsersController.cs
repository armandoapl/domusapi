using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Extensions;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepo, IMapper mapper, IPhotoService photoService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _photoService = photoService;
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
        [HttpGet("{username}", Name ="GetUser")]
        public async Task<ActionResult<AgentDto>> GetUser(string username)
        {
            //var user = await _userRepo.GetUserByUsernameAsync(username);
            //var userToReturn = _mapper.Map<AgentDto>(user);

            //return Ok(userToReturn);

            return await _userRepo.GetAgentByUsernameAsync(username);
        }

        [Authorize]
        [HttpGet("get-by-id")]
        public async Task<ActionResult> GetUserById([FromQuery] int id)
        {
            return Ok(await _userRepo.GetUserByIdAsync(id));
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

        

        [Authorize]
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            //User.GetUserName();// extension method in the extensions folder

            var user = await _userRepo.GetUserByUsernameAsync(User.GetUserName());

            var result = await _photoService.AddPhotoAsync(file, true);

            if(result.Error != null)
                return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
                photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _userRepo.SaveAllAsync())
            {
               return CreatedAtRoute("GetUser", new {username = user.UserName}, _mapper.Map<PhotoDto>(photo));
               
            }

            return BadRequest("Problem Adding the Photo.");


        }
    }
}
