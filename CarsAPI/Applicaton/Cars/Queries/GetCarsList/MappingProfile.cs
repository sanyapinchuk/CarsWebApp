using AutoMapper;
using Domain;
using Shared.Dto;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarListDto>()
                .ForMember(carDto => carDto.Id,
                    mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Price,
                    mem => mem.MapFrom(car => car.Price))
                .ForMember(carDto => carDto.ModelName,
                    mem => mem.MapFrom(car => car.Model.Name))
                .ForMember(carDto => carDto.TitleImagePath,
                    mem => mem.MapFrom(src => src.Car_Images.Where(ci => ci.IsMainImage).First().Image.Path))
                .ForMember(carDto => carDto.Properties,
                    mem => mem.MapFrom(src => src.Car_PropValues
                        .Where(cpv => cpv.PropValue.Property.IsKeyProperty)
                        .Select<Car_PropValue, PropertyDto>(cpv =>
                            new PropertyDto()
                            {
                                Value = cpv.PropValue.Value,
                                Property = cpv.PropValue.Property.Name
                            }
                        ).ToList()));
        }
    }
}
