using MediatR;

namespace Applicaton.News.Commands
{
    public class CreateNewsCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ContentHtml { get; set; }
    }
}
