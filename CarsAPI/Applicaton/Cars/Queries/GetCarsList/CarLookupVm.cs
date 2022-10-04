using Applicaton.Common.Exceptions;
using Applicaton.Common.Mappings;
using Applicaton.Interfaces;
using AutoMapper;
using Domain;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class CarLookupVm : IMapWith<Car>
    {
        private readonly IDataContext _dataContext;
        public CarLookupVm(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Guid Id { get; set; }
        //public int Price { get; set; }
        public string ModelName { get; set; }
        public string TitleImagePath { get; set; }

        public List<string> Colors { get; set; }

        public List<(string propName, string propValue)> properties { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarLookupVm>()
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
                .ForMember(carDto => carDto.TitleImagePath,
                mem => mem.MapFrom((src, dst) =>
                {
                    var imageTitle = _dataContext.Images
                    .Where(i => i.CarId == src.Id)
                    .FirstOrDefault();

                    if (imageTitle == null)
                        throw new DamagedEntityException(nameof(Car), "Image", src.Id);

                    var imageExtansion = imageTitle.Path.Substring(imageTitle.Path.LastIndexOf('.') + 1);

                    var subStringBeforeLastSlash = imageTitle.Path.Substring(0,
                        imageTitle.Path.LastIndexOf('/'));

                    return subStringBeforeLastSlash + "/1." + imageExtansion;
                }))
                .ForMember(carDto => carDto.properties,
                mem => mem.MapFrom((src, dst) =>
                {
                    var car__prop_propValues = _dataContext.Car__Property_PropValues
                        .Where(cppv => cppv.CarId == src.Id).ToList();

                    if (car__prop_propValues == null)
                        throw new DamagedEntityException(nameof(Car), "Properties", src.Id);

                    List<(string propName, string propValue)> listProperties = new();


                    var allCar__prop_propValues = _dataContext.Car__Property_PropValues
                    .Where(cppv => cppv.CarId == src.Id)
                    .ToList();
                    var prop_values = from cppv in _dataContext.Car__Property_PropValues
                                      where cppv.CarId == src.Id
                                      join ppv in _dataContext.Property_PropValues
                                      on cppv.Property_PropValueId equals ppv.Id
                                      join p in _dataContext.Properties
                                      on ppv.PropertyId equals p.Id
                                      join pv in _dataContext.PropValues
                                      on ppv.PropValueId equals pv.Id
                                      where p.Name == "Запас хода" // all short info properties
                                      select new { propertyName = p.Name, propertyValue = pv.Value };

                    foreach (var propValue in prop_values)
                    {
                        listProperties.Add((propValue.propertyName, propValue.propertyValue));
                    }
                    return listProperties;
                }))
                .ForMember(carDto => carDto.Colors,
                mem => mem.MapFrom((src, dst) =>
                {
                    var car_colors = _dataContext.Car_Colors
                    .Where(cc => cc.CarId == src.Id)
                    .ToList();
                    var allColors = _dataContext.Colors.ToList();
                    List<string> colorsResult = new();
                    car_colors.ForEach(cc =>
                    {
                        var color = allColors.FirstOrDefault(all => all.Id == cc.ColorId);
                        if (color == null)
                            throw new DamagedEntityException(nameof(Car_Color),
                                "Color",
                                cc.ColorId);

                        colorsResult.Add(color.Name);
                    });

                    return colorsResult;
                }));
        }

    }
}
