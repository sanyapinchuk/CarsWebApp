using Applicaton.Interfaces;
using MediatR;

namespace Applicaton.News.Commands;

public class DeleteNewsCommandHandler: IRequestHandler<DeleteNewsCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteNewsCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Unit> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        await _repositoryManager.NewsRepository.Delete(request.NewsId);
        await _repositoryManager.SaveAsync();

        return Unit.Value;
    }
}