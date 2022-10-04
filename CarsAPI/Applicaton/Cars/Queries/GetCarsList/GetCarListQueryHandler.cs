

using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Applicaton.Interfaces;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class GetCarListQueryHandler
        : IRequestHandler<GetCarListQuery, CarListDto>
    {
        private readonly IMapper _mapper;
        private readonly IDataContext _dataContext;
        public GetCarListQueryHandler(IDataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<CarListDto> Handle(GetCarListQuery request, 
            CancellationToken cancellationToken)
        {
            var cars = await _dataContext.Cars
                .ProjectTo<CarLookupVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CarListDto() { Cars = cars };
        }
    }
}
