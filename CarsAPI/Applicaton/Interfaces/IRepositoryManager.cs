using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface IRepositoryManager
    {
        Task SaveAsync();
        ICarImageRepository CarImageRepository { get; }
        ICarPropValueRepository CarPropValueRepository { get; }
        ICarRepository CarRepository { get; }
        ICarTypeRepository CarTypeRepository { get; }
        IImageRepository ImageRepository { get; }
        IModelRepository ModelRepository { get; }
        IPropValueRepository PropValueRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        INewsRepository NewsRepository { get; }
    }
}
