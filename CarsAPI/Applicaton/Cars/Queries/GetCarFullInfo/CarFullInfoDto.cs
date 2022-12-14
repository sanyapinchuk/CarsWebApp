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
    public class CarFullInfoDto:IMapWith<Car>
    {
        private readonly IDataContext _dataContext;
        public CarFullInfoDto(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Guid Id { get; set; }
        //public int Price { get; set; }
        public string ModelName { get; set; }
        public List<string> PathImages{ get; set; } 
        public List<(string propName, string propValue)> properties { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarFullInfoDto>()
                .ForMember(carDto => carDto.Id,
                mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.ModelName,
                mem => mem.MapFrom((src, dst) =>
                {
                    var modelName = _dataContext.Models.Where(m => m.Id == src.ModelId)
                                     .FirstOrDefault();
                    if (modelName == null)
                        throw new DamagedEntityException(nameof(Car), "Model", new object());

                    return modelName.Name;
                }))
                .ForMember(carDto => carDto.PathImages,
                mem => mem.MapFrom((src, dst) =>
                {
                    var images = _dataContext.Images
                    .Where(i => i.CarId == src.Id)
                    .ToList();

                    if (images == null)
                        throw new DamagedEntityException(nameof(Car), "Image", src.Id);
                    var resultList = new List<string>();
                    foreach (var image in images)
                        resultList.Add(image.Path);

                    return resultList;
                }))
                .ForMember(carDto => carDto.properties,
                mem => mem.MapFrom((src, dst) =>
                {
                    var car__prop_propValues = _dataContext.Car__Property_PropValues
                        .Where(cppv => cppv.CarId == src.Id).ToList();

                    if (car__prop_propValues == null)
                        throw new DamagedEntityException(nameof(Car), "Properties", src.Id);

                    List<(string propName, string propValue)> listProperties = new();

                    var allprop_propValue = _dataContext.Property_PropValues.ToList();
                    var allPropeties = _dataContext.Properties.ToList();
                    var allPropValues = _dataContext.PropValues.ToList();
                    car__prop_propValues.ForEach(cppv =>
                    {
                        //all prop_propValue for this car
                        var prop_propValue = allprop_propValue
                            .Where(all => all.Id == cppv.Property_PropValueId)
                            .FirstOrDefault();

                        if (prop_propValue == null)
                            throw new DamagedEntityException(nameof(Car__Property_PropValue),
                                "Property_PropValue",
                                cppv.Property_PropValueId);

                        var property = allPropeties
                        .Where(all => all.Id == prop_propValue.PropertyId)
                        .FirstOrDefault();

                        if (property == null)
                            throw new DamagedEntityException(nameof(Property_PropValue),
                                "Property",
                                prop_propValue.PropertyId);

                        var propValue = allPropValues
                        .Where(all => all.Id == prop_propValue.PropValueId)
                        .FirstOrDefault();

                        if (propValue == null)
                            throw new DamagedEntityException(nameof(Property_PropValue),
                                "Value",
                                prop_propValue.PropertyId);

                        listProperties.Add(new()
                        {
                            propName = property.Name,
                            propValue = propValue.Value
                        });

                    });

                    return listProperties;
                }));
        }

    }
}
