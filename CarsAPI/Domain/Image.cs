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

        public Guid CarId { get; set; }
        public virtual Car? Car { get; set; }
    }
}
