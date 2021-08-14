using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Extensions;
using Tesis.Helpers;
using Tesis.Interfaces;

namespace Tesis.Controllers
{
    public class PropertiesController : BaseApiController
    {
        private readonly IPropertyRepository _propertyRepo;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PropertiesController(IPropertyRepository propertyRepo, IMapper mapper, IPhotoService photoService)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
            _photoService = photoService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PropertyDto>> RegisterProperty(PropertyDto property)
        {
            var propertyToReturn = await _propertyRepo.AddPropertyAsync(property);

            return Ok(propertyToReturn);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties([FromQuery] UserPropertiesParams userParams)
        {
            var properties = await _propertyRepo.GetPropertiesDtoAsync(userParams);

            Response.AddPaginatioHeader(properties.CurrentPage, properties.PageSize, properties.TotalCount, properties.TotalPages);

            return Ok(properties);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            return Ok(await _propertyRepo.GetPropertyDtoByIdAsync(id));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateProperty(int idProperty)
        {
            return Ok();
        }

        [Authorize]
        [HttpDelete("{propertyId}")]
        public async Task<ActionResult> DeteleProperty(int propertyId)
        {
            var response =await  _propertyRepo.DeletePropertyAsync(propertyId);

            if (response == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Something Unexpected happened");
            }
            
        }

        [Authorize]
        [HttpPost("add-photo/{propertyId}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int propertyId)
        {
            //User.GetUserName();// extension method in the extensions folder

            var property = await _propertyRepo.GetPropertyByIdAsync(propertyId);

            var result = await _photoService.AddPhotoAsync(file, false);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (property.Photos.Count == 0)
                photo.IsMain = true;

            property.Photos.Add(photo);

            if (await _propertyRepo.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser", new { username = property.Title }, _mapper.Map<PhotoDto>(photo));

            }

            return BadRequest("Problem Adding the Photo.");
        }

        [Authorize]
        [HttpPut("set-main-photo/{photoId}/{propertyId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId, int propertyId)
        {
            var property =  await _propertyRepo.GetPropertyByIdAsync(propertyId);
            var photo = property.Photos.FirstOrDefault(ph => ph.Id == photoId);

            if (photo.IsMain)
                return BadRequest("This is already your main photo");

            var currentMain = property.Photos.FirstOrDefault(ph => ph.IsMain);

            if (currentMain != null)
                currentMain.IsMain = false;

            photo.IsMain = true;

            if (await _propertyRepo.SaveAllAsync())
                return NoContent();

            return BadRequest("Faild to set main photo");
        }

        [Authorize]
        [HttpDelete("delete-photo/{photoId}/{propertyId}")]
        public async Task<ActionResult> DeletePhoto(int photoId, int propertyId)
        {
            //encapsular esta logica en property repository para luego llamar este metodo de 
            //delete property-photo aqui ene ste metodo y en el metodo de delete property
            var property = await _propertyRepo.GetPropertyByIdAsync(propertyId);
            var photo = property.Photos.FirstOrDefault(ph => ph.Id == photoId);

            if (photo == null)
                return NotFound();

            if (photo.IsMain)
                return BadRequest("Your cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var results = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (results.Error != null)
                    return BadRequest(results.Error.Message);
            }

            property.Photos.Remove(photo);

            if (await _propertyRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photos");
        }
    }
}
