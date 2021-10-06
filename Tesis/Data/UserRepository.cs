using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Helpers;
using Tesis.Interfaces;

namespace Tesis.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Properties)
                .Include(ph => ph.Photos)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => 
                    p.Properties).ThenInclude(x => x.Photos )
                .Include(ph => ph.Photos)
                .FirstOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task AddUserAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0? true: false;
        }


        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<PagedList<AgentDto>> GetAgentsAsync(UserPropertiesParams userParams)
        {
            var query = _context.Users.AsQueryable();
            
            query = query.Where(u => u.UserName!= userParams.TitleName);

            return await PagedList<AgentDto>.CreateAsync(query.ProjectTo<AgentDto>(_mapper.ConfigurationProvider)
                .AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AgentDto> GetAgentByUsernameAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username).Include(x => x.Photos)
                .ProjectTo<AgentDto>(_mapper.ConfigurationProvider).
                SingleOrDefaultAsync();
        
        }

        public async Task<string[]> GetCities()
        {
            var cities = _context.Users.Select(user => user.City).ToArray();
            return cities;
        }
    }
}
