using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        public String Path { get; set; } = String.Empty;

        public virtual List<Car_Image>? Car_Images { get; set; }
    }
}
