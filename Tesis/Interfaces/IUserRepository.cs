using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;

namespace Tesis.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user); // to update the profile of a user.
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task AddUserAsync(AppUser user);
        Task<bool> UserExistsAsync(string username);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AgentDto>> GetAgentsAsync();
        Task<AgentDto> GetAgentByUsernameAsync(string username);
    }
}
