using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Applicaton.Cars.Queries.GetCarsList;
using Applicaton.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repository
{
    public class CarRepository : BaseEntityRepository<Car>, ICarRepository
    {
        private readonly IMapper _mapper;

        public CarRepository(DataContext dataContext, IMapper mapper) : base(dataContext)
        {
            _mapper = mapper;
        }

        public async Task<Guid> Create(Car car)
        {
            await _dataContext.Cars.AddAsync(car);
            return car.Id;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            var list = await _dataContext.Cars
                .Include(x=>x.Car_PropValues)
                .Include(x=>x.Model)
                .Include(x=>x.Car_Images)
                .ToListAsync();
            return list;
        }

        public async Task<IEnumerable<CarListDto>> GetAllCarsDtoAsync()
        {
            return await _dataContext.Cars
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<CarListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllCarsWithQueryAsync(Expression<Func<Car, bool>> predicate)
        {
            var list = await _dataContext.Cars
                .Where(predicate)
                .ToListAsync();
            return list;
        }

        public async Task<IEnumerable<CarListDto>> GetAllCarsDtoWithQueryAsync(Expression<Func<Car, bool>> predicate)
        {
            return await _dataContext.Cars
                .OrderByDescending(x => x.CreatedAt)
                .Where(predicate)
                .ProjectTo<CarListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Guid> UpdateCarAsync(Car car)
        {
            _dataContext.Entry(car).State = EntityState.Modified;
            _dataContext.Cars.Update(car);
            return car.Id;
        }
    }
}
