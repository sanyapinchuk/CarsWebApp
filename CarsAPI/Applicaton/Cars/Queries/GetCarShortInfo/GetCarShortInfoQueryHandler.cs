using Applicaton.Common.Exceptions;
using Applicaton.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Applicaton.Cars.Queries.GetCarShortInfo
{
    public class GetCarShortInfoQueryHandler
        : IRequestHandler<GetCarShortInfoQuery, CarShortInfoDto>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;
        public GetCarShortInfoQueryHandler(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper; 
        }
        public async Task<CarShortInfoDto> Handle(GetCarShortInfoQuery request, 
            CancellationToken cancellationToken)
        {
            var car = await _dataContext.Cars.FirstOrDefaultAsync(c => c.Id == request.Id,
                cancellationToken);
            if(car == null)
                throw new EntityNotFoundException(nameof(car),request.Id);
            return _mapper.Map<CarShortInfoDto>(car);
        }
    }
}
