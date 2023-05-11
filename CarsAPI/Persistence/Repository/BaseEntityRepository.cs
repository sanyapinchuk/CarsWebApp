using Applicaton.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class BaseEntityRepository<T> : IBaseEntityRepository<T> where T: BaseEntity
    {
        protected readonly DataContext _dataContext;
        public BaseEntityRepository(DataContext dataContext)
        {
            _dataContext= dataContext;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var entity = await _dataContext.Set<T>().FindAsync(id);
            _dataContext.Set<T>().Remove(entity);
            return id;
        }

        public async Task<List<Guid>> DeleteByCondition(Expression<Func<T, bool>> condition)
        {
            var entities = _dataContext.Set<T>().Where(condition);
            _dataContext.Set<T>().RemoveRange(entities);
            var listId = new List<Guid>();
            foreach (var entity in entities)
            {
                listId.Add(entity.Id);
            }
            return listId;
        }

        public async Task<IList<T>> GetAllByCondition(Expression<Func<T, bool>> condition)
        {
            return await _dataContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<T?> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return await _dataContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }
    }
}
