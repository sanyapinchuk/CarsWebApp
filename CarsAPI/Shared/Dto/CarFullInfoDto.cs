using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class CarFullInfoDto
    {
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string CarType { get; set; }
        public int ProductionYear { get; set; }
        public string CompanyName { get; set; }
        public string Color { get; set; }
        public List<FullPropertyDto> Properties { get; set; }
        public List<ImageInfoDto> Images { get; set; }
    }
}
