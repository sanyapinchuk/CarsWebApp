using Applicaton.Interfaces;
using Domain;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Persistence.Repository
{
    public class ImageRepository : BaseEntityRepository<Image>, IImageRepository
    {
        public ImageRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(string path)
        {
            var id = Guid.NewGuid();
            await _dataContext.Images.AddAsync(new Image() { Id = id, Path = path});
            return id;
        }
    }
}
