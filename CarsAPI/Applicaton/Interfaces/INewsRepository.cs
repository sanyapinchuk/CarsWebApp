using Domain;

namespace Applicaton.Interfaces
{
    public interface INewsRepository: IBaseEntityRepository<Domain.News>
    {
        Task<Guid> Create(Domain.News car);
        Task<Guid> UpdateNewsAsync(Domain.News car);
        Task<IEnumerable<Domain.News>> GetAllNewsAsync();
    }
}
