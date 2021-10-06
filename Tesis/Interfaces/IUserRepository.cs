using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Helpers;

namespace Tesis.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user); // to update the profile of a user.
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<string[]> GetCities();
        Task AddUserAsync(AppUser user);
        Task<bool> UserExistsAsync(string username);
        Task<bool> SaveAllAsync();
        Task<PagedList<AgentDto>> GetAgentsAsync(UserPropertiesParams userparams);
        Task<AgentDto> GetAgentByUsernameAsync(string username);
    }
}
