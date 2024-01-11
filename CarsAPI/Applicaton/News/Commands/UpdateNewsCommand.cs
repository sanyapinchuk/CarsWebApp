using MediatR;

namespace Applicaton.News.Commands
{
    public class UpdateNewsCommand:IRequest
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ContentHtml { get; set; }
    }
}
