using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Interfaces
{
    public interface IBaseEntityRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<Guid> Delete(Guid id);
        Task<T?> GetByCondition(Expression<Func<T, bool>> condition);
        Task<IList<T>> GetAllByCondition(Expression<Func<T, bool>> condition);
        Task<List<Guid>> DeleteByCondition(Expression<Func<T, bool>> condition);
    }
}
