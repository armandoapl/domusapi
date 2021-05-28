using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesis.Entities;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    public class PropertiesController : BaseApiController
    {
        private readonly IPropertyRepository _propertyRepo;

        public PropertiesController(IPropertyRepository propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<REProperty>>> GetProperties()
        {
            return Ok(await _propertyRepo.GetPropertiesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<REProperty>> GetProperty(int id)
        {
            return Ok(await _propertyRepo.GetPropertyByIdAsync(id));
        }

    }
}
