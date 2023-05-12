using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Car_PropValue: BaseEntity
    {
        public Guid CarId { get; set; }
        public virtual Car? Car { get; set; }

        public Guid PropValueId { get; set; }
        public virtual PropValue? PropValue { get; set; }
    }
}
