using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface IBaseEntityRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<Guid> Delete(Guid id);
        Task<T?> GetByCondition(Func<T, bool> condition);
        Task<IList<T>> GetAllByCondition(Func<T, bool> condition);
        Task<List<Guid>> DeleteByCondition(Func<T, bool> condition);
    }
}
