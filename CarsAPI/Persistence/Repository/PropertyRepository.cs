using Applicaton.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Persistence.Repository
{
    public class PropertyRepository : BaseEntityRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(string name, bool isKeyProperty, string category, int priority)
        {

            var id = Guid.NewGuid();
            await _dataContext.Properties.AddAsync(new Property
            {
                Id = id,
                Name = name,
                IsKeyProperty = isKeyProperty, 
                PropCategoryId = await GetOrAddCategoryId(category, priority)
            });

            return id;
        }

        private async Task<Guid> GetOrAddCategoryId(string category, int priority)
        {
            var existCategory = await _dataContext.PropCategories.Where(x=>x.Name == category && x.Priority == priority)
                .FirstOrDefaultAsync();  

            if (existCategory == null)
            {
                existCategory = new()
                {
                    Name = category,
                    Priority = priority
                };
                await _dataContext.PropCategories.AddAsync(existCategory);
            }

            return existCategory.Id;
        }
    }
}
