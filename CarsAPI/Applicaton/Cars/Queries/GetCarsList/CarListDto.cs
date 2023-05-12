using Applicaton.Common.Exceptions;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using AutoMapper;
using Domain;
using Shared.Dto;
using System;
using System.Linq;
using System.Net.Mail;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class CarListDto : IMapWith<Car>
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string CompanyName { get; set; }
        public string TitleImagePath { get; set; }
        public string Color { get; set; }
        public List<PropertyDto> Properties { get; set; }

        public async void Mapping(Profile profile, IRepositoryManager repositoryManager)
        {
            profile.CreateMap<Car, CarListDto>()
                .ForMember(carDto => carDto.Id,
                mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Price,
                mem => mem.MapFrom(car => car.Price))
                .ForMember(carDto => carDto.ModelName,
                mem => mem.MapFrom(car => car.Model.Name))
                .ForMember(carDto => carDto.CompanyName,
                mem => mem.MapFrom(car => car.Model.Company.Name))
                .ForMember(carDto => carDto.TitleImagePath,
                 mem => mem.MapFrom(src => src.Car_Images.Where(ci => ci.IsMainImage).First().Image.Path))
                 .ForMember(carDto => carDto.Properties,
                  mem => mem.MapFrom(src => src.Car_PropValues
                    .Where(cpv => cpv.PropValue.Property.IsKeyProperty)
                    .Select<Car_PropValue, PropertyDto>(cpv =>
                        new PropertyDto() 
                        { 
                            Value= cpv.PropValue.Value, 
                            Property = cpv.PropValue.Property.Name
                        }
                  ).ToList()))
                .ForMember(carDto => carDto.Color,
                mem => mem.MapFrom(src => src.Color.Name));
        }
    }
}
