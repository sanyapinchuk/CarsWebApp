using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Applicaton.Cars.Queries.GetCarShortInfo
{
    public class GetCarShortInfoQuery : IRequest<CarShortInfoDto>
    {
        public Guid Id { get; set; }
    }
}
