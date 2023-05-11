using Applicaton.Interfaces;
using Domain;
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

        public async Task<Guid> Create(string name, bool isKeyProperty)
        {
            var id = Guid.NewGuid();
            await _dataContext.Properties.AddAsync(new Property
            {
                Id = id,
                Name = name,
                IsKeyProperty = isKeyProperty
            });

            return id;
        }
    }
}
