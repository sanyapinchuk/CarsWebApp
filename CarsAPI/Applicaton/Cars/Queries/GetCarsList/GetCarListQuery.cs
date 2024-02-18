 using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class GetCarListQuery:IRequest<CarListVm>
    {
        public Guid UserId { get; set; }

        public Guid[]? CarTypeIds { get; set; }
         
        public Guid[]? PowerReserveParamIds { get; set; }
         
        public int? PriceFrom { get; set; }
         
        public int? PriceTo { get; set; }

        public Guid[]? BatteryCapacityIds { get; set; }

        public Guid[]? ManufacturesIds { get; set; }

        public Guid[]? DriveModeIds { get; set; }
    }
}
