using Applicaton.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repository
{
    public class CarRepository : BaseEntityRepository<Car>, ICarRepository
    {
        public CarRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(Car car)
        {
            await _dataContext.Cars.AddAsync(car);
            return car.Id;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            var list = await _dataContext.Cars.ToListAsync();
            return list;
        }

        public async Task<Guid> UpdateCarAsync(Car car)
        {
            _dataContext.Entry(car).State = EntityState.Modified;
            _dataContext.Cars.Update(car);
            return car.Id;
        }
    }
}
