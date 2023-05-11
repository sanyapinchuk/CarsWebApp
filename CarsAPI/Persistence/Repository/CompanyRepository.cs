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
    public class CompanyRepository : BaseEntityRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Guid> Create(string Name)
        {
            var id = Guid.NewGuid();
            await _dataContext.Companies.AddAsync(new Company { Id = id, Name = Name });
            return id;
        }
    }
}
