using MediatR;

namespace Applicaton.News.Commands
{
    public class DeleteNewsCommand:IRequest
    {
        public Guid NewsId { get; set; }
    }
}
