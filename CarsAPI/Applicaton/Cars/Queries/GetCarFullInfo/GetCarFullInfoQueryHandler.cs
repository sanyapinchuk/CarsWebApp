using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applicaton.Cars.Queries.GetCarsList;
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
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public GetCarFullInfoQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<CarFullInfoDto> Handle(GetCarFullInfoQuery request,
            CancellationToken cancellationToken)
        {
            var car = await _repositoryManager.CarRepository.GetById(request.Id);
            if (car == null)
                throw new EntityNotFoundException(nameof(car), request.Id);

            return _mapper.Map<CarFullInfoDto>(car); //TODO IRepository in DI container
        }
    }
}
