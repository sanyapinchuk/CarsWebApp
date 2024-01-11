using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface IModelRepository : IBaseEntityRepository<Model>
    {
        Task<Guid> Create(string name, Guid carTypeId);
    }
}
