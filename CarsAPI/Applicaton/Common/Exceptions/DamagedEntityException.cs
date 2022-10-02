using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Common.Exceptions
{
    public class DamagedEntityException:Exception
    {
        public DamagedEntityException(string entityName, string fieldName, object key)
            : base($"In entity {entityName} field {fieldName} ({key}) wasnot found,  ")
        {

        }
    }
}
