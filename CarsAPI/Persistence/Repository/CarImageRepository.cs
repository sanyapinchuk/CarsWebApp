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
    public class CarImageRepository: BaseEntityRepository<Car_Image>, ICarImageRepository
    {
        public CarImageRepository(DataContext dataContext): base(dataContext)
        {
        }

        public async Task<Guid> Create(Guid carId, Guid ImageId, bool IsMainImage)
        {
            var id = Guid.NewGuid();
            await _dataContext.Car_Images.AddAsync(new Car_Image()
            {
                CarId = carId,
                ImageId = ImageId,
                IsMainImage = IsMainImage
            });
            return id;
        }
    }
}
