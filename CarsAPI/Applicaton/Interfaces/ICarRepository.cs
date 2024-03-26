using Applicaton.Cars.Queries.GetCarsList;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface ICarRepository : IBaseEntityRepository<Car>
    {
        Task<Guid> Create(Car car);
        Task<Guid> UpdateCarAsync(Car car);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<IEnumerable<CarListDto>> GetAllCarsDtoAsync();
        Task<IEnumerable<Car>> GetAllCarsWithQueryAsync(Expression<Func<Car, bool>> predicate);
        Task<IEnumerable<CarListDto>> GetAllCarsDtoWithQueryAsync(Expression<Func<Car, bool>> predicate);
    }
}
