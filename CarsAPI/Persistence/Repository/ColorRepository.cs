using Applicaton.Interfaces;
using Domain;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ColorRepository : BaseEntityRepository<Color>, IColorRepository
    {
        public ColorRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(string name)
        {
            var id = Guid.NewGuid();
            await _dataContext.AddAsync(new Color { Id = id, Name = name });
            return id;
        }
    }
}
