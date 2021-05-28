﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tesis.Entities;

namespace Tesis.Interfaces
{
    public interface IPropertyRepository
    {
        void Update(REProperty property);
        Task<IEnumerable<REProperty>> GetPropertiesAsync();
        Task<REProperty> GetPropertyByIdAsync(int id);
        Task<bool> SaveAllAsync();
    }
}
