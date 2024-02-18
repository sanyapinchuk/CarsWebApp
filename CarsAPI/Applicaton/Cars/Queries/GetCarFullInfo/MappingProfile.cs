using AutoMapper;
using Domain;
using Shared.Dto;

namespace Applicaton.Cars.Queries.GetCarFullInfo
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarFullInfoDto>()
                .ForMember(carDto => carDto.Id,
                mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Price,
                mem => mem.MapFrom(car => car.Price))
                .ForMember(car => car.ProductionYear,
                mem => mem.MapFrom(car => car.ProductionYear))
                .ForMember(carDto => carDto.Description,
                mem => mem.MapFrom(car => car.Description))
                .ForMember(carDto => carDto.CarType,
                mem => mem.MapFrom(car => car.Model.CarType.Name))
                .ForMember(carDto => carDto.ModelName,
                mem => mem.MapFrom(car => car.Model.Name))
                .ForMember(carDto => carDto.Images,
                mem => mem.MapFrom(src => src.Car_Images
                    .Select<Car_Image, ImageInfoDto>(ci =>
                    new ImageInfoDto()
                    {
                        IsMainImage = ci.IsMainImage,
                        Path = ci.Image.Path
                    })))
                .ForMember(carDto => carDto.Properties,
                mem => mem.MapFrom(src => src.Car_PropValues
                .Select<Car_PropValue, FullPropertyDto>(cpv =>
                    new FullPropertyDto()
                    {
                        Value = cpv.PropValue.Value,
                        Property = cpv.PropValue.Property.Name,
                        IsKeyProperty = cpv.PropValue.Property.IsKeyProperty,
                        Category = cpv.PropValue.Property.PropCategory.Name,
                        Priority = cpv.PropValue.Property.PropCategory.Priority
                    }
                   ).ToList()))
                .ForMember(carDto => carDto.SameCars,
                mem => mem.MapFrom(src => src.Model.Cars.Where(c => c.Id != src.Id)
                    .Select<Car, SameCarInfoDto>(c =>
                    new SameCarInfoDto()
                    {
                        Id = c.Id,
                        ModelName = c.Model.Name,
                        TitleImage = c.Car_Images.Where(ci => ci.IsMainImage).First().Image.Path
                    })));
        }
    }
}
