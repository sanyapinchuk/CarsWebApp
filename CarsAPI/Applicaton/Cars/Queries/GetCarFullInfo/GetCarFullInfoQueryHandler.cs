using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applicaton.Common.Exceptions;
using Applicaton.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applicaton.Cars.Queries.GetCarFullInfo
{
    public class GetCarFullInfoQueryHandler
        : IRequestHandler<GetCarFullInfoQuery, CarFullInfoDto>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;
        public GetCarFullInfoQueryHandler(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<CarFullInfoDto> Handle(GetCarFullInfoQuery request,
            CancellationToken cancellationToken)
        {
            var car = await _dataContext.Cars.FirstOrDefaultAsync(c => c.Id == request.Id,
                cancellationToken);
            if (car == null)
                throw new EntityNotFoundException(nameof(car), request.Id);
            return _mapper.Map<CarFullInfoDto>(car);
        }
    }
}
