using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tesis.Entities;
using Tesis.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tesis.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;
        public PropertyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<REProperty>> GetPropertiesAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<REProperty> GetPropertyByIdAsync(int id)
        {
            return await _context.Properties.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0? true : false;
        }

        public void Update(REProperty property)
        {
            _context.Entry(property).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
 }
