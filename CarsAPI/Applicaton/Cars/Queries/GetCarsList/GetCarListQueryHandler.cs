

using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Applicaton.Interfaces;
using System.Data.Common;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class GetCarListQueryHandler
        : IRequestHandler<GetCarListQuery, CarListVm>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public GetCarListQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            
            _repositoryManager = repositoryManager;
        }
        public async Task<CarListVm> Handle(GetCarListQuery request,
            CancellationToken cancellationToken)
        {
            var carsQ = await _repositoryManager.CarRepository.GetAllCarsAsync();
            var cars = new List<CarListDto>();
            foreach (var car in carsQ)
            {
                cars.Add(_mapper.Map<CarListDto>(car));
            }
            return new CarListVm() { Cars = cars };
        }
    }
}
