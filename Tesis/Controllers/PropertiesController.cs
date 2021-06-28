using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    public class PropertiesController : BaseApiController
    {
        private readonly IPropertyRepository _propertyRepo;
        private readonly IMapper _mapper;

        public PropertiesController(IPropertyRepository propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties()
        {
            //var properties = await  _propertyRepo.GetPropertiesAsync();
            //var propertiesToReturn = _mapper.Map<IEnumerable<PropertyDto>>(properties);
            //return Ok(propertiesToReturn);
            return Ok(await _propertyRepo.GetPropertiesDtoAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            //var property = await _propertyRepo.GetPropertyByIdAsync(id);
            //var propertyToReturn = _mapper.Map<PropertyDto>(property);
            //return Ok(propertyToReturn);
            return Ok(await _propertyRepo.GetPropertyDtoByIdAsync(id));
        }

    }
}
