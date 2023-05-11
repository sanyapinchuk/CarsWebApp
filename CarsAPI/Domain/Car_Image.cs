using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Car_Image: BaseEntity
    {
        public bool IsMainImage { get; set; }

        public Guid ImageId { get; set; }
        public virtual Image? Image { get; set; }
        public Guid CarId { get; set; }
        public virtual Car? Car { get; set; }
    }
}
