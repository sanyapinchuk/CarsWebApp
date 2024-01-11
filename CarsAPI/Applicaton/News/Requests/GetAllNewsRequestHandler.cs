using Applicaton.Interfaces;
using AutoMapper;
using MediatR;

namespace Applicaton.News.Requests;

public class GetAllNewsRequestHandler: IRequestHandler<GetAllNewsRequestHandler.Request, List<NewsDto>>
{
    public record Request : IRequest<List<NewsDto>>;

    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GetAllNewsRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<List<NewsDto>> Handle(Request request, CancellationToken cancellationToken)
    {
        var news = await _repositoryManager.NewsRepository.GetAllNewsAsync();

        return news
            .Select(n => _mapper.Map<NewsDto>(n)).ToList();
    }
}
