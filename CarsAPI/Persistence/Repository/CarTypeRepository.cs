using Applicaton.Interfaces;
using Domain;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class CarTypeRepository : BaseEntityRepository<CarType>, ICarTypeRepository
    {
        public CarTypeRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(string Name)
        {
            var id = Guid.NewGuid();
            await _dataContext.CarTypes.AddAsync(new CarType { Id = id, Name = Name });
            return id;
        }
    }
}
