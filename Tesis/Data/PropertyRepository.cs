using System.Collections.Generic;
//using System.Data.Entity;
using System.Threading.Tasks;
using Tesis.Entities;
using Tesis.Interfaces;
using Microsoft.EntityFrameworkCore;
using Tesis.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Linq;

namespace Tesis.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PropertyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<REProperty>> GetPropertiesAsync()
        {
            return await _context.Properties.Include(x=> x.Photos).ToListAsync();
        }


        public async Task<REProperty> GetPropertyByIdAsync(int id)
        {
            return await _context.Properties.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0? true : false;
        }

        public void Update(REProperty property)
        {
            _context.Entry(property).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }


        public async Task<IEnumerable<PropertyDto>> GetPropertiesDtoAsync()
        {
            return await  _context.Properties
                .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


        public async Task<PropertyDto> GetPropertyDtoByIdAsync(int id)
        {
            return await _context.Properties
                .Where( p=> p.Id == id)
                .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
 }
