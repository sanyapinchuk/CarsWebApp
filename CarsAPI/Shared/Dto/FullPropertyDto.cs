using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class FullPropertyDto
    {
        public string Property { get; set; }
        public bool IsKeyProperty { get; set; }
        public string Value { get; set; }
    }
}
