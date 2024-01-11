using Applicaton.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repository
{
    public class NewsRepository : BaseEntityRepository<News>, INewsRepository
    {
        public NewsRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(News news)
        {
            await _dataContext.News.AddAsync(news);

            return news.Id;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _dataContext.News.ToListAsync();
        }

        public Task<Guid> UpdateNewsAsync(News news)
        {
            _dataContext.Entry(news).State = EntityState.Modified;
            _dataContext.News.Update(news);
            return Task.FromResult(news.Id);
        }
    }
}
