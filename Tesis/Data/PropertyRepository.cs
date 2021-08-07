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
using System;
using Tesis.Helpers;

namespace Tesis.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper; 
        private readonly IPhotoService _photoService;
        public PropertyRepository(DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
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


        public async Task<PagedList<PropertyDto>> GetPropertiesDtoAsync(UserPropertiesParams propertiesParams)
        {
            var query =  _context.Properties
                .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider).AsNoTracking();

            return await PagedList<PropertyDto>.CreateAsync(query, propertiesParams.PageNumber, propertiesParams.PageSize);
        }


        public async Task<PropertyDto> GetPropertyDtoByIdAsync(int id)
        {
            return await _context.Properties
                .Where( p=> p.Id == id)
                .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PropertyDto> AddPropertyAsync(PropertyDto property)
        {
            var propertyToRegister = new REProperty();
            propertyToRegister.Title = property.Title;
            propertyToRegister.Description = property.Description;
            propertyToRegister.City = property.City;
            propertyToRegister.Country = property.Country;
            propertyToRegister.AppUserId = property.AppUserId;
            propertyToRegister.Address = property.Address;
            propertyToRegister.CreatedAt = DateTime.Now;

             
            await _context.Properties.AddAsync(propertyToRegister);
             if (await _context.SaveChangesAsync() > 0)
             {
                property.Id = propertyToRegister.Id;
                return property;
             }
            else{
                throw new Exception("Could'n save the property");
            }
        }

        public async Task<bool> DeletePropertyAsync(int propertyId)
        {
            var propertyToDelete = await GetPropertyByIdAsync(propertyId);
            _context.Remove(propertyToDelete);

            //var photos = propertyToDelete.Photos;

            //if(photos != null)
            //{
            //    foreach(Photo photo in photos)
            //    {
            //        var results = await _photoService.DeletePhotoAsync(photo.PublicId);
            //        if (results.Error == null)
            //            throw new Exception("Problema al borrar fotos de la propiedad numero " + propertyToDelete.Id);
            //    }
            //}

            if (await SaveAllAsync())
                return true;
            else return false;

        }
    }
 }
