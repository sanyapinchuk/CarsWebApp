using MediatR;
using AutoMapper;
using Applicaton.Interfaces;
using Applicaton.Common.Helpers;
using Domain;
using LinqKit;

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
            var predicate = PredicateBuilder.New<Car>(true);

            if (request.CarTypeIds?.Any() == true)
            {
                var needToAddFilter = false;
                var subQuery = PredicateBuilder.New<Car>(true);

                foreach (var typeFilterId in request.CarTypeIds)
                {
                    var query = ApplicationHelper.CarFilterConfigDto.CarFilerTypeConfigs
                        .FirstOrDefault(x => x.Id == typeFilterId)?.Query;

                    if (query != null)
                    {
                        subQuery = subQuery.Or(query);
                        needToAddFilter = true;
                    }
                }

                if (needToAddFilter)
                {
                    predicate = predicate.And(subQuery);
                }
            }

            if (request.PowerReserveParamIds?.Any() == true)
            {
                var needToAddFilter = false;
                var subQuery = PredicateBuilder.New<Car>(true);

                foreach (var powerFilterId in request.PowerReserveParamIds)
                {
                    var query = ApplicationHelper.CarFilterConfigDto.CarFilerPowerReserveConfigs
                        .FirstOrDefault(x => x.Id == powerFilterId)?.Query;

                    if (query != null)
                    {
                        subQuery = subQuery.Or(query);
                        needToAddFilter = true;
                    }
                }

                if (needToAddFilter)
                {
                    predicate = predicate.And(subQuery);
                }
            }

            if (request.BatteryCapacityIds?.Any() == true)
            {
                var needToAddFilter = false;
                var subQuery = PredicateBuilder.New<Car>(true);

                foreach (var batteryFilterId in request.BatteryCapacityIds)
                {
                    var query = ApplicationHelper.CarFilterConfigDto.CarFilerBatteryCapacityConfigs
                        .FirstOrDefault(x => x.Id == batteryFilterId)?.Query;

                    if (query != null)
                    {
                        subQuery = subQuery.Or(query);
                        needToAddFilter = true;
                    }
                }

                if (needToAddFilter)
                {
                    predicate = predicate.And(subQuery);
                }
            }

            if(request.ManufacturesIds?.Any() == true)
            {
                var needToAddFilter = false;
                var subQuery = PredicateBuilder.New<Car>(true);
                foreach (var manufacturerFilterId in request.ManufacturesIds)
                {
                    var query = ApplicationHelper.CarFilterConfigDto.CarFilerManufacturersConfigs
                        .FirstOrDefault(x => x.Id == manufacturerFilterId)?.Query;

                    if (query != null)
                    {
                        subQuery = subQuery.Or(query);
                        needToAddFilter = true;
                    }
                }

                if (needToAddFilter)
                {
                    predicate = predicate.And(subQuery);
                }
            }

            if (request.DriveModeIds?.Any() == true)
            {
                var needToAddFilter = false;
                var subQuery = PredicateBuilder.New<Car>(true);
                foreach (var driveModeFilterId in request.DriveModeIds)
                {
                    var query = ApplicationHelper.CarFilterConfigDto.CarFilerDriveModeConfigs
                        .FirstOrDefault(x => x.Id == driveModeFilterId)?.Query;

                    if (query != null)
                    {
                        subQuery = subQuery.Or(query);
                        needToAddFilter = true;
                    }
                }

                if (needToAddFilter)
                {
                    predicate = predicate.And(subQuery);
                }
            }

            if (request.PriceFrom.HasValue)
            {
                predicate = predicate.And(x => x.Price >= request.PriceFrom);
            }

            if (request.PriceTo.HasValue)
            {
                predicate = predicate.And(x => x.Price <= request.PriceTo); 
            }

            var cars = await _repositoryManager.CarRepository.GetAllCarsDtoWithQueryAsync(predicate);

            return new CarListVm() { Cars = cars.ToList() };
        }
    }

    public static class PredicateExtensions
    {
        public static Func<T, bool> AndAlso<T>(this Func<T, bool> left, Func<T, bool> right)
        {
            return x => left(x) && right(x);
        }
    }
}
