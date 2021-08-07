using System.Collections.Generic;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Helpers;

namespace Tesis.Interfaces
{
    public interface IPropertyRepository
    {
        void Update(REProperty property);
        Task<IEnumerable<REProperty>> GetPropertiesAsync();
        Task<REProperty> GetPropertyByIdAsync(int id);
        Task<bool> SaveAllAsync();
        Task<PropertyDto> GetPropertyDtoByIdAsync(int id);
        Task<PagedList<PropertyDto>> GetPropertiesDtoAsync(UserPropertiesParams propertiesParams);
        Task<PropertyDto> AddPropertyAsync(PropertyDto property);
        Task<bool> DeletePropertyAsync(int propertyId);
    }
}
