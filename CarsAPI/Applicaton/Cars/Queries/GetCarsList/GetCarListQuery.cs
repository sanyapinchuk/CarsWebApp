using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class GetCarListQuery:IRequest<CarListDto>
    {

    }
}
