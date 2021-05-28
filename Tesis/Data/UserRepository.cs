using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesis.Entities;
using Tesis.Interfaces;

namespace Tesis.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
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
                .Include(p => p.Properties)
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
    }
}
