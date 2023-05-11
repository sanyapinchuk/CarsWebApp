using Applicaton.Common.Exceptions;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using AutoMapper;
using Domain;
using System.Linq;

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
        public List<(string propertyName, string value)> Properties { get; set; }

        public async void Mapping(Profile profile, IRepositoryManager repositoryManager)
        {
            profile.CreateMap<Car, CarListDto>()
                .ForMember(carDto => carDto.Id,
                mem => mem.MapFrom(car => car.Id))
                .ForMember(carDto => carDto.Price,
                mem => mem.MapFrom(car => car.Price))
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
                .ForMember(carDto => carDto.TitleImagePath,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var ciId = (await repositoryManager.CarImageRepository
                      .GetByCondition(ci => ci.IsMainImage && ci.CarId == src.Id)).ImageId;
                    var imageTitle = (await repositoryManager.ImageRepository.GetByCondition(i => i.Id == ciId)).Path;
                   
                    if (imageTitle == null)
                        throw new DamagedEntityException(nameof(Car), "Image", src.Id);

                    var imageExtansion = imageTitle.Substring(imageTitle.LastIndexOf('.') + 1);

                    var subStringBeforeLastSlash = imageTitle.Substring(0,
                        imageTitle.LastIndexOf('/'));

                    return subStringBeforeLastSlash + "/1." + imageExtansion;
                }))
                .ForMember(carDto => carDto.Properties,
                mem => mem.MapFrom(async (src, dst) =>
                {
                    var car_propValues = await repositoryManager.CarPropValueRepository.GetAllByCondition(cpv => cpv.CarId == src.Id);
                    if (car_propValues == null)
                        throw new DamagedEntityException(nameof(Car), "Properties", src.Id);

                    List<(string propName, string propValue)> listProperties = new();

                    foreach(var car_propValue in car_propValues)
                    {
                        var value = car_propValue.PropValue.Value;
                        var property = car_propValue.PropValue.Property;
                        if (property.IsKeyProperty)
                        {
                            listProperties.Add((property.Name, value));
                        }
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
