using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Applicaton.Cars.Queries.GetCarFullInfo
{
    public class GetCarFullInfoQuery : IRequest<CarFullInfoDto>
    {
        public Guid Id { get; set; }
    }
}
