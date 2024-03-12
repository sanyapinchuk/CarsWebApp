using MediatR;
using Applicaton.Common.Helpers;

namespace Applicaton.Cars.Queries.GetCarFilterConfig;

public class GetCarFilterConfigQueryHandler: IRequestHandler<GetCarFilterConfigQueryHandler.Request, CarFilterConfigDto>
{
    public record Request : IRequest<CarFilterConfigDto>;

    public Task<CarFilterConfigDto> Handle(Request request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ApplicationHelper.CarFilterConfigDto);
    }
}