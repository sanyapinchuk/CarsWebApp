using Applicaton.Cars.Queries.GetCarsList;
using Applicaton.Common.Exceptions;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
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
        public Guid Id;
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string CarType { get; set; }
        public int ProductionYear { get; set; }
        public string CompanyName { get; set; }
        public string Color { get; set; }
        public List<(string propertyName, bool isKeyProperty, string value)> Properties { get; set; }
        public List<(string path, bool isMainImage)> Images { get; set; }
        public void Mapping(Profile profile, IRepositoryManager repositoryManager)
        {
            profile.CreateMap<Car, CarFullInfoDto>()
                .ForMember(carDto => carDto.Id,
                mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Price,
                mem => mem.MapFrom(car => car.Price))
                .ForMember(carDto => carDto.ProductionYear,
                mem => mem.MapFrom(car => car.ProductionYear))
                .ForMember(carDto => carDto.CarType,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var model = await repositoryManager.ModelRepository.GetByCondition(m => m.Id == src.ModelId);

                    if (model == null)
                        throw new DamagedEntityException(nameof(Car), "CarType", new object());

                    return model.CarType.Name;
                }))
                .ForMember(carDto => carDto.ModelName,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var modelName = (await repositoryManager.ModelRepository.GetByCondition(m => m.Id == src.ModelId)).Name;

                    if (modelName == null)
                        throw new DamagedEntityException(nameof(Car), "Model", new object());

                    return modelName;
                }))
                .ForMember(carDto => carDto.CompanyName,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var model = await repositoryManager.ModelRepository.GetByCondition(c => c.Id == src.ModelId);

                    if (model == null)
                        throw new DamagedEntityException(nameof(Car), "Company", new object());
                    var companyName = model.Company.Name;

                    return companyName;
                }))
                .ForMember(carDto => carDto.Images,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var car_images = (await repositoryManager.CarImageRepository
                      .GetAllByCondition(ci => ci.IsMainImage && ci.CarId == src.Id));

                    var imageList = new List<(string path, bool isMainImage)>();
                    foreach(var c_i in car_images)
                    {
                        imageList.Add((c_i.Image.Path, c_i.IsMainImage));
                    }
                }))
                .ForMember(carDto => carDto.Properties,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var car_propValues = await repositoryManager.CarPropValueRepository
                        .GetAllByCondition(cpv => cpv.CarId == src.Id);
                    if (car_propValues == null)
                        throw new DamagedEntityException(nameof(Car), "Properties", src.Id);

                    List<(string propName, bool isKeyProperty, string propValue)> listProperties = new();

                    foreach (var car_propValue in car_propValues)
                    {
                        var value = car_propValue.PropValue.Value;
                        var property = car_propValue.PropValue.Property;
                        listProperties.Add((property.Name, property.IsKeyProperty, value));
                    }

                    return listProperties;
                }))
                .ForMember(carDto => carDto.Color,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var color = (await repositoryManager.ColorRepository.GetByCondition(c => c.Id == src.ColorId)).Name;
                    return color;
                }));
        }

    }
}
