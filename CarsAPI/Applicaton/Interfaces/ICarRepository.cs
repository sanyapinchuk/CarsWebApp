using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface ICarRepository : IBaseEntityRepository<Car>
    {
        Task<Guid> Create(Car car);
        Task<Guid> UpdateCarAsync(Car car);
        Task<IEnumerable<Car>> GetAllCarsAsync();
    }
}
