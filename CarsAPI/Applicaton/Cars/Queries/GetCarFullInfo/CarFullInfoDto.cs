using Applicaton.Cars.Queries.GetCarsList;
using Applicaton.Common.Exceptions;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Cars.Queries.GetCarFullInfo
{
    public class CarFullInfoDto : IMapWith<Car>
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string CarType { get; set; }
        public int ProductionYear { get; set; }
        public string CompanyName { get; set; }
        public string Color { get; set; }
        public List<FullPropertyDto> Properties { get; set; }
        public List<ImageInfoDto> Images { get; set; }
        public void Mapping(Profile profile, IRepositoryManager repositoryManager)
        {
            profile.CreateMap<Car, CarFullInfoDto>()
                .ForMember(carDto => carDto.Id,
                mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Price,
                mem => mem.MapFrom(car => car.Price))
                .ForMember(car => car.ProductionYear,
                mem => mem.MapFrom(car => car.ProductionYear))
                .ForMember(carDto => carDto.CarType,
                mem => mem.MapFrom(car => car.Model.CarType.Name))
                .ForMember(carDto => carDto.ModelName,
                mem => mem.MapFrom(car => car.Model.Name))
                .ForMember(carDto => carDto.CompanyName,
                mem => mem.MapFrom(car => car.Model.Company.Name))
                .ForMember(carDto => carDto.Images,
                mem => mem.MapFrom(src => src.Car_Images
                    .Select<Car_Image, ImageInfoDto>(ci=>
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
                        IsKeyProperty = cpv.PropValue.Property.IsKeyProperty
                    }
                   ).ToList()))
                .ForMember(carDto => carDto.Color,
                mem => mem.MapFrom(src => src.Color.Name));
        }

    }
}
