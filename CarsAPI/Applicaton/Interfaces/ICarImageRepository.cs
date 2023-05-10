using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface ICarImageRepository: IBaseEntityRepository<Car_Image>
    {
        Task<Guid> Create(Guid carId, Guid ImageId, bool IsMainImage);
    }
}
