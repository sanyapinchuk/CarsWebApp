using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Cars.Commands.CreateCar
{
    public class CreateCarCommand : IRequest<Guid>
    {
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string CompanyName { get; set; }
        public List<string> Colors { get; set; }
        public List<(string propertyName, string value)> Properties { get; set; }
        public List<string> Images { get; set; }
    }
}
