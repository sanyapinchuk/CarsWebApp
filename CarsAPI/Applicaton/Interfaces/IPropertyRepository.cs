using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface IPropertyRepository : IBaseEntityRepository<Property>
    {
        Task<Guid> Create(string name, bool isKeyProperty, string category, int priority);
    }
}
