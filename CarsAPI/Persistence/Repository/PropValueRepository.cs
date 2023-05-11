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
    public class PropValueRepository : BaseEntityRepository<PropValue>, IPropValueRepository
    {
        public PropValueRepository(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<Guid> Create(string name, Guid propId)
        {
            var id = Guid.NewGuid();
            await _dataContext.PropValues.AddAsync(new PropValue
            {
                Id = id,
                Value = name,
                PropertyId = propId
            });

            return id;
        }
    }
}
