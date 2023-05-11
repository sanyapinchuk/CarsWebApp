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
    public class ModelRepository : BaseEntityRepository<Model>, IModelRepository
    {
        public ModelRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(string name, Guid companyId, Guid carTypeId)
        {
            var id = Guid.NewGuid();
            await _dataContext.Models.AddAsync(new Model
            {
                Id = id,
                Name = name,
                CompanyId = companyId,
                CarTypeId = carTypeId
            });

            return id;
        }
    }
}
