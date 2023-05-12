using MediatR;
using Domain;
using Applicaton.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Shared.Dto;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Applicaton.Cars.Commands.CreateCar
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {


        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = new Car()
            {
                Id = new Guid(),
                Price = request.CreateCarDto.Price,
                ProductionYear = request.CreateCarDto.ProductionYear,
            };
            //set model
            var modelId = (await _repositoryManager.ModelRepository.GetByCondition(m => m.Name == request.CreateCarDto.ModelName))?.Id;
            if(modelId == null || modelId == Guid.Empty)
            {
                var carTypeId = (await _repositoryManager.CarTypeRepository.GetByCondition(ct => ct.Name == request.CreateCarDto.CarType))?.Id;
                if(carTypeId == null || carTypeId == Guid.Empty)
                {
                    carTypeId = await _repositoryManager.CarTypeRepository.Create(request.CreateCarDto.CarType);
                }
                var companyId = (await _repositoryManager.CompanyRepository.GetByCondition(c=>c.Name==request.CreateCarDto.CompanyName))?.Id;
                if (companyId == null || companyId == Guid.Empty)
                {
                    companyId = await _repositoryManager.CompanyRepository.Create(request.CreateCarDto.CompanyName);
                }

                modelId = await _repositoryManager.ModelRepository.Create(request.CreateCarDto.ModelName, (Guid)companyId, (Guid)carTypeId);
            }
            car.ModelId = (Guid)modelId;

            //set color
            var colorId = (await _repositoryManager.ColorRepository.GetByCondition(c => c.Name == request.CreateCarDto.Color))?.Id;
            if (colorId == null || colorId == Guid.Empty)
            {
                colorId = await _repositoryManager.ColorRepository.Create(request.CreateCarDto.Color);
            }
            car.ColorId = (Guid)colorId;


            await _repositoryManager.CarRepository.Create(car);


            //set images
            foreach (var image in request.CreateCarDto.Images)
            {
                var imageId = (await _repositoryManager.ImageRepository.GetByCondition(i => i.Path == image.Path))?.Id;
                if (imageId == null || imageId == Guid.Empty)
                {
                    imageId = await _repositoryManager.ImageRepository.Create(image.Path);
                }
                await _repositoryManager.CarImageRepository.Create(car.Id, (Guid)imageId, image.IsMainImage);
            }

            //set properties

            foreach (var props in request.CreateCarDto.Properties)
            {
                var propertyId = (await _repositoryManager.PropertyRepository.GetByCondition(p => p.Name == props.Property))?.Id;
                if (propertyId == null || propertyId == Guid.Empty)
                {
                    propertyId = await _repositoryManager.PropertyRepository.Create(props.Property, props.IsKeyProperty);
                }
                var valueId = (await _repositoryManager.PropValueRepository.GetByCondition(p => p.Value == props.Value))?.Id;
                if (valueId == null || valueId == Guid.Empty)
                {
                    valueId = await _repositoryManager.PropValueRepository.Create(props.Value, (Guid)propertyId);
                }

                await _repositoryManager.CarPropValueRepository.Create(car.Id, (Guid)valueId);
            }

            await _repositoryManager.SaveAsync();
            return car.Id;
        }


        /*



        private readonly IDataContext _dataContext;

        public CreateCarCommandHandler(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var model = await Task.Run(() => _dataContext.Models
            .Where(m => m.Name == request.ModelName)
            .FirstOrDefault());

            //create new modelW
            if(model== null)
            {
                var company = await  _dataContext.Companies
                .Where(c => c.Name == request.CompanyName)
                .FirstOrDefaultAsync();

                //create new company
                if(company== null)
                {
                    company = new Company()
                    {
                        Id = Guid.NewGuid(),
                        Name = request.CompanyName
                    };
                    await _dataContext.Companies.AddAsync(company);
                }
                model = new Model()
                {
                    Name = request.ModelName,
                    Id = Guid.NewGuid(),
                    ComponyId = company.Id
                };
                await _dataContext.Models.AddAsync(model);
            }

            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Price = request.Price,
                ModelId = model.Id
            };
            await _dataContext.Cars.AddAsync(car);

            request.Images.ForEach(image => _dataContext.Images.Add(new()
            {
                Id = Guid.NewGuid(),
                Path = image,
                CarId = car.Id
            }));

            request.Properties.ForEach(async property =>
            {
                bool maybeExist = true;
                //get property
                var prop = await  _dataContext.Properties
                .Where(p => p.Name == property.propertyName)
                .FirstOrDefaultAsync();
                if (prop == null)
                {
                    maybeExist = false;
                    //create new property
                    prop = new Property()
                    {
                        Id = Guid.NewGuid(),
                        Name = property.propertyName
                    };
                    await _dataContext.Properties.AddAsync(prop);

                }
                //get property value
                var propValue = await _dataContext.PropValues
                    .Where(pv => pv.Value == property.value)
                    .FirstOrDefaultAsync();
                if (propValue == null)
                {
                    maybeExist = false;
                    //create new value
                    propValue = new PropValue()
                    {
                        Id = Guid.NewGuid(),
                        Value = property.value
                    };
                    await _dataContext.PropValues.AddAsync(propValue);
                }

                //create car__prop_propValue

                //check maybe prop_propValue already exist
                var prop_propValue = await _dataContext.Property_PropValues
                    .Where(ppv => ppv.PropValueId == propValue.Id && ppv.PropertyId == prop.Id)
                    .FirstOrDefaultAsync();
                if (prop_propValue == null)
                {
                    prop_propValue = new Property_PropValue()
                    {
                        Id = Guid.NewGuid(),
                        PropertyId = prop.Id,
                        PropValueId = propValue.Id
                    };
                }
                var car__prop_propValue = new Car__Property_PropValue()
                {
                    Id = Guid.NewGuid(),
                    CarId = car.Id,
                    Property_PropValueId = prop_propValue.Id,
                };
                await _dataContext.Car__Property_PropValues
                .AddAsync(car__prop_propValue);

            });

            await _dataContext.SaveChangesAsync(cancellationToken);
            return car.Id;
        }*/
    }
}
