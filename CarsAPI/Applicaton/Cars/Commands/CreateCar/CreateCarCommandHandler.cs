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
                Description = request.CreateCarDto.Description,
                PageTitle = request.CreateCarDto.PageTitle,
                PageDescription = request.CreateCarDto.PageDescription
            };
            //set model
            var modelId = (await _repositoryManager.ModelRepository.GetByCondition(m => m.Name == request.CreateCarDto.ModelName))?.Id;
            if (modelId == null || modelId == Guid.Empty)
            {
                var carTypeId = (await _repositoryManager.CarTypeRepository.GetByCondition(ct => ct.Name == request.CreateCarDto.CarType))?.Id;
                if (carTypeId == null || carTypeId == Guid.Empty)
                {
                    carTypeId = await _repositoryManager.CarTypeRepository.Create(request.CreateCarDto.CarType);
                }

                modelId = await _repositoryManager.ModelRepository.Create(request.CreateCarDto.ModelName, (Guid)carTypeId);
            }
            car.ModelId = (Guid)modelId;

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
                var propertyId = (await _repositoryManager.PropertyRepository.GetByCondition(p =>
                    p.Name == props.Property && p.IsKeyProperty == props.IsKeyProperty
                    && p.PropCategory.Name == props.Category && p.PropCategory.Priority == props.Priority))?.Id;
                if (propertyId == null || propertyId == Guid.Empty)
                {
                    propertyId = await _repositoryManager.PropertyRepository.Create(props.Property, props.IsKeyProperty, props.Category, props.Priority);
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
    }
}
