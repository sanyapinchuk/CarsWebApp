using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Applicaton.Common.Exceptions;
using Applicaton.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applicaton.Cars.Commands.UpdateCar
{
    public class UpdateCarComamndHandler : IRequestHandler<UpdateCarCommand>
    {
        private readonly IDataContext _dataContext;
        private readonly IRepositoryManager _repositoryManager;
        public UpdateCarComamndHandler(IDataContext dataContext, IRepositoryManager repositoryManager)
        {
            this._dataContext = dataContext;
            _repositoryManager = repositoryManager;
        }
        // TODO rewrite copy code
        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _repositoryManager.CarRepository.GetById(request.Id);
            if (car == null)
                return Unit.Value;

            //set model
            var modelId = (await _repositoryManager.ModelRepository.GetByCondition(m => m.Name == request.CarInfo.ModelName))?.Id;
            if (modelId == null || modelId == Guid.Empty)
            {
                var carTypeId = (await _repositoryManager.CarTypeRepository.GetByCondition(ct => ct.Name == request.CarInfo.CarType))?.Id;
                if (carTypeId == null || carTypeId == Guid.Empty)
                {
                    carTypeId = await _repositoryManager.CarTypeRepository.Create(request.CarInfo.CarType);
                }
                var companyId = (await _repositoryManager.CompanyRepository.GetByCondition(c => c.Name == request.CarInfo.CompanyName))?.Id;
                if (companyId == null || companyId == Guid.Empty)
                {
                    companyId = await _repositoryManager.CompanyRepository.Create(request.CarInfo.CompanyName);
                }

                modelId = await _repositoryManager.ModelRepository.Create(request.CarInfo.ModelName, (Guid)companyId, (Guid)carTypeId);
            }
            car.ModelId = (Guid)modelId;

            //set images
            await _repositoryManager.CarImageRepository.DeleteByCondition(ci=>ci.CarId== car.Id);
            foreach (var image in request.CarInfo.Images)
            {
                var imageId = (await _repositoryManager.ImageRepository.GetByCondition(i => i.Path == image.Path))?.Id;
                if (imageId == null || imageId == Guid.Empty)
                {
                    imageId = await _repositoryManager.ImageRepository.Create(image.Path);
                }
                await _repositoryManager.CarImageRepository.Create(car.Id, (Guid)imageId, image.IsMainImage);
            }

            //set properties
            await _repositoryManager.CarPropValueRepository.DeleteByCondition(ci => ci.CarId == car.Id);
            foreach (var props in request.CarInfo.Properties)
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

            //set color

            var colorId = (await _repositoryManager.ColorRepository.GetByCondition(c => c.Name == request.CarInfo.Color))?.Id;
            if (colorId == null || colorId == Guid.Empty)
            {
                colorId = await _repositoryManager.ColorRepository.Create(request.CarInfo.Color);
            }
            car.ColorId = (Guid)colorId;

            car.ProductionYear = request.CarInfo.ProductionYear;
            car.Price = request.CarInfo.Price;
            car.Description = request.CarInfo.Description;

            await _repositoryManager.CarRepository.UpdateCarAsync(car);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }

    }
}
