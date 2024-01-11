using Applicaton.Common.Exceptions;
using Applicaton.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.News.Commands
{
    public class UpdateNewsCommandHandler: IRequestHandler<UpdateNewsCommand>
    {
        private readonly IRepositoryManager _repositoryManager;

        public UpdateNewsCommandHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Unit> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _repositoryManager.NewsRepository.GetById(request.Id);

            if (news == null)
            {
                throw new EntityNotFoundException(nameof(News), request.Id);
            }
            news.ShortDescription = request.ShortDescription;
            news.ContentHtml = request.ContentHtml;
            news.Title = request.Title;

            await _repositoryManager.NewsRepository.UpdateNewsAsync(news);
            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
