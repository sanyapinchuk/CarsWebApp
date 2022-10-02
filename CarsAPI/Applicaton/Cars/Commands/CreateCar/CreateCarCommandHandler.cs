using MediatR;
using Domain;
using Applicaton.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Applicaton.Cars.Commands.CreateCar
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {
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

            //create new model
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
        }
    }
}
