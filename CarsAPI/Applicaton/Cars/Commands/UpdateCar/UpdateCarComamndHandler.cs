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

            var carColorsDb = await _dataContext.Car_Colors
                .Where(cc=>cc.CarId == car.Id)
                .ToListAsync();
            //delete exist colors in Db for this car 
            carColorsDb.ForEach(cc =>
            {
                if(cc.Color==null)
                    throw new DamagedEntityException(nameof(Car_Color), "Color", cc.Id);

                if (!request.Colors.Contains(cc.Color.Name))
                    _dataContext.Car_Colors.Remove(cc);
            });
            //add to db new not exist colors for this car 
            request.Colors.ForEach(async c =>
            {
                var dbColor = await _dataContext.Colors
                .Where(allC => allC.Name == c)
                .FirstOrDefaultAsync();
                if (dbColor == null)
                {
                    //add new color 
                    dbColor = new Color()
                    {
                        Id = Guid.NewGuid(),
                        Name = c
                    };
                    _dataContext.Colors.Add(dbColor);
                }
                try
                {
                    if (!carColorsDb.Where(cc => cc.Color.Name == c).Any())
                    {
                        //add car_color to db
                        var car_color = new Car_Color()
                        {
                            Id = Guid.NewGuid(),
                            CarId = car.Id,
                            ColorId = dbColor.Id
                        };
                        _dataContext.Car_Colors.Add(car_color);
                    }
                }
                catch (NullReferenceException)
                {
                    throw new DamagedEntityException(nameof(Car_Color), "Color", new object());
                }
            });

            //properties
            
            var car_propDb = await _dataContext.Car__Property_PropValues
                .Where(cpv => cpv.CarId == car.Id)
                .ToListAsync();
            //delete exist unnecessary properties for this car 
            car_propDb.ForEach(cpv =>
            {
                if (!request.Properties.Contains((cpv.Property_PropValue.Property.Name,
                    cpv.Property_PropValue.PropValue.Value)))
                {
                    _dataContext.Car__Property_PropValues.Remove(cpv);
                }
            });
            //add to db not exist propeprties for this car
            request.Properties.ForEach(async reqProp =>
            {
                if (!car_propDb.Where(cpDb => cpDb.Property_PropValue.Property.Name == reqProp.propertyName
                 && cpDb.Property_PropValue.PropValue.Value == reqProp.value).Any())
                {
                    //need to add car__property_propValue
                    //check maybe already exist property_propValue in db
                    var prop_propValueDb = await _dataContext.Property_PropValues
                        .Where(ppv => ppv.Property.Name == reqProp.propertyName
                        && ppv.PropValue.Value == reqProp.value).FirstOrDefaultAsync();
                    if (prop_propValueDb == null)
                    {
                        //need to add prop_propValue

                        //check maybe exist necessary propertty in Db 
                        var propertyDb = await _dataContext.Properties
                        .Where(p => p.Name == reqProp.propertyName).FirstOrDefaultAsync();
                        if (propertyDb == null)
                        {
                            //need add property
                            propertyDb = new Property()
                            {
                                Id = Guid.NewGuid(),
                                Name = reqProp.propertyName
                            };
                            _dataContext.Properties.Add(propertyDb);
                        }
                        //check maybe exist necessary propertyValue in Db 
                        var propertyValueDb = await _dataContext.PropValues
                        .Where(p => p.Value == reqProp.propertyName).FirstOrDefaultAsync();
                        if (propertyValueDb == null)
                        {
                            //need add propertyValue
                            propertyValueDb = new PropValue()
                            {
                                Id = Guid.NewGuid(),
                                Value = reqProp.value
                            };
                            _dataContext.PropValues.Add(propertyValueDb);
                        }

                        //add prop_propValue in db 
                        prop_propValueDb = new Property_PropValue()
                        {
                            Id = Guid.NewGuid(),
                            PropertyId = propertyDb.Id,
                            PropValueId = propertyValueDb.Id
                        };
                        _dataContext.Property_PropValues.Add(prop_propValueDb);
                    }
                    var car__property_propValue = new Car__Property_PropValue()
                    {
                        Id = Guid.NewGuid(),
                        CarId = car.Id,
                        Property_PropValueId = prop_propValueDb.Id
                    };
                    _dataContext.Car__Property_PropValues.Add(car__property_propValue);
                }
            });

            await _dataContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
