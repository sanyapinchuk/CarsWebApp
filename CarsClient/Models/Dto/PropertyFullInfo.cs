using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsClient.Models.Dto
{
    public class PropertyFullInfo
    {
        public string Property { get; set; }
        public bool IsKeyProperty { get; set; }
        public string Value { get; set; }
    }
}
