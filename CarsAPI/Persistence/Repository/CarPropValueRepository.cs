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
    public class CarPropValueRepository: BaseEntityRepository<Car_PropValue>, ICarPropValueRepository
    {
        public CarPropValueRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(Guid carId, Guid propValueId)
        {
            var id = Guid.NewGuid();
            await _dataContext.Car_PropValues
                .AddAsync(new Car_PropValue()
                {
                    CarId = carId,
                    PropValueId = propValueId
                });
            return id;
        }
    }
}
