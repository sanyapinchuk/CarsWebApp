using Applicaton.Interfaces;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataContext _dataContext;

        private CarImageRepository carImageRepository;
        private CarPropValueRepository carPropValueRepository;
        private CarRepository carRepository;
        private CarTypeRepository carTypeRepository;
        private CompanyRepository companyRepository;
        private ImageRepository imageRepository;
        private ModelRepository modelRepository;
        private PropValueRepository propValueRepository;
        private PropertyRepository propertyRepository;
        private ColorRepository colorRepository;

        public RepositoryManager(DataContext dataContext) 
        {
            _dataContext= dataContext;
        }
        public ICarImageRepository CarImageRepository
        {
            get
            {
                if (carImageRepository == null)
                    return new CarImageRepository(_dataContext);
                return carImageRepository;
            }
        }

        public ICarPropValueRepository CarPropValueRepository
        {
            get
            {
                if (carPropValueRepository == null)
                    return new CarPropValueRepository(_dataContext);
                return carPropValueRepository;
            }
        }
        public ICarRepository CarRepository
        {
            get
            {
                if (carRepository == null)
                    return new CarRepository(_dataContext);
                return carRepository;
            }
        }
        public ICarTypeRepository CarTypeRepository
        {
            get
            {
                if (carTypeRepository == null)
                    return new CarTypeRepository(_dataContext);
                return carTypeRepository;
            }
        }
        public ICompanyRepository CompanyRepository
        {
            get
            {
                if (companyRepository == null)
                    return new CompanyRepository(_dataContext);
                return companyRepository;
            }
        }
        public IImageRepository ImageRepository
        {
            get
            {
                if (imageRepository == null)
                    return new ImageRepository(_dataContext);
                return imageRepository;
            }
        }
        public IModelRepository ModelRepository
        {
            get
            {
                if (modelRepository == null)
                    return new ModelRepository(_dataContext);
                return modelRepository;
            }
        }
        public IPropValueRepository PropValueRepository
        {
            get
            {
                if (propValueRepository == null)
                    return new PropValueRepository(_dataContext);
                return propValueRepository;
            }
        }
        public IPropertyRepository PropertyRepository
        {
            get
            {
                if (propertyRepository == null)
                    return new PropertyRepository(_dataContext);
                return propertyRepository;
            }
        }
        public IColorRepository ColorRepository
        {
            get
            {
                if (colorRepository == null)
                    return new ColorRepository(_dataContext);
                return colorRepository;
            }
        }
        public async Task SaveAsync()
        {
           await _dataContext.SaveChangesAsync();
        }
    }
}
