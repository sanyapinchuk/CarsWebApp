using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface ICarPropValueRepository: IBaseEntityRepository<Car_PropValue>
    {
        Task<Guid> Create(Guid carId, Guid propValueId);
    }
}
