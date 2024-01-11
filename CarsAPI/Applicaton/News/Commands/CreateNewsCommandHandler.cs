using Applicaton.Interfaces;
using MediatR;

namespace Applicaton.News.Commands;

public class CreateNewsCommandHandler: IRequestHandler<CreateNewsCommand, Guid>
{
    public record Response;

    private readonly IRepositoryManager _repositoryManager;

    public CreateNewsCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Guid> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = new Domain.News
        {
            ContentHtml = request.ContentHtml,
            Title = request.Title,
            ShortDescription = request.ShortDescription,
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid()
        };
        await _repositoryManager.NewsRepository.Create(news);
        await _repositoryManager.SaveAsync();

        return news.Id;
    }
}