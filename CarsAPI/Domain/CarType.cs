using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CarType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public virtual List<Model>? Models { get; set; }
    }
}
