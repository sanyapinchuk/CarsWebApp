using Applicaton.Interfaces;
using AutoMapper;
using MediatR;

namespace Applicaton.News.Requests;

public class GetNewsRequestHandler : IRequestHandler<GetNewsRequestHandler.Request, NewsDto>
{
    public record Request(Guid Id) : IRequest<NewsDto>;

    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GetNewsRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<NewsDto> Handle(Request request, CancellationToken cancellationToken)
    {
        var news = await _repositoryManager.NewsRepository.GetById(request.Id);

        return _mapper.Map<NewsDto>(news);
    }
}