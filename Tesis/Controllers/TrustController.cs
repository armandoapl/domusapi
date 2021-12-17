using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tesis.Extensions;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    [Authorize]
    public class TrustController : BaseApiController
    {
        private readonly IUserRepository _userRepo;
        private readonly ITrustRepository _trustRepo;

        public TrustController(IUserRepository userRepo, ITrustRepository trustRepo)
        {
            _userRepo = userRepo;
            _trustRepo = trustRepo;
        }

        //[HttpPost("{username}")]
        //public async Task<ActionResult> AddLike(string username)
        //{
        //    var sourceUserId = User

        //    var likedUser = await _userRepo.GetUserByUsernameAsync(username);
        //    var sourceUsrId = await _userRepo.getuser
        //    //var sourceUser = await _userRepo.getuse

        //    return Ok();
        //}
    }
}
