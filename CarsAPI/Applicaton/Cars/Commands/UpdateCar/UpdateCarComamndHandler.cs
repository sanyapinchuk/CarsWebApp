using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public UpdateCarComamndHandler(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _dataContext.Cars.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (car == null)
            {
                throw new EntityNotFoundException(nameof(Car), request.Id);
            }
            
            var modelDb = await _dataContext.Models
                .FirstOrDefaultAsync(m => m.Id == car.ModelId);

            if(modelDb == null)
            {
                throw new DamagedEntityException(nameof(Car), nameof(Model), car.ModelId);
            }
            if(request.ModelName != modelDb.Name)
            {
                modelDb.Name = request.ModelName;
                _dataContext.Models.Update(modelDb);/*
                //delete old model 

                //check eist other models of this company
                var modelsOfComapny = await _dataContext.Models
                    .Where(m=>m.ComponyId == modelDb.ComponyId)
                    .ToListAsync();
                if(modelsOfComapny.Count == 0 || modelsOfComapny.Count == 1)
                {
                    //delete company and cascade Model
                    _dataContext.Companies.Remove(new Company() { Id = modelDb.ComponyId });
                }
                _dataContext.Models.Remove(modelDb);*/
            }

            if(modelDb.Company.Name != request.CompanyName)
            {
                var requiredCompany = await _dataContext.Companies
                    .Where(c => c.Name == request.CompanyName)
                    .FirstOrDefaultAsync();
                if(requiredCompany != null)
                {
                    //reasign other comapny
                    modelDb.ComponyId = requiredCompany.Id;
                }
                else
                {
                    //create new Company
                    var newCompany = new Company()
                    {
                        Id = Guid.NewGuid(),
                        Name = request.CompanyName
                    };
                    modelDb.ComponyId = newCompany.Id;
                }
                _dataContext.Models.Update(modelDb);
            }
            var imagesDb = await _dataContext.Images
                .Where(img => img.CarId == car.Id)
                .ToListAsync();

            //delete from Db old unnecessary images
            imagesDb.ForEach(img =>
            {
                if (!request.Images.Contains(img.Path))
                    _dataContext.Images.Remove(img);
            });
            //add to db new not exist images
            request.Images.ForEach(imgPath =>
            {
                if (!imagesDb.Where(img => img.Path == imgPath).Any())
                {
                    _dataContext.Images.Add(new Image()
                    {
                        Path = imgPath,
                        Id = Guid.NewGuid(),
                        CarId = car.Id
                    });
                }
            });
            //TODO properties and colors


            return Unit.Value;
        }
    }
}
