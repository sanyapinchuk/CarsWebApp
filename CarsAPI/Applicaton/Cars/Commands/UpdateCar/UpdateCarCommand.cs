using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Applicaton.Cars.Commands.UpdateCar
{
    public class UpdateCarCommand : IRequest
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public string CompanyName { get; set; }
        public List<string> Colors { get; set; }
        public List<(string propertyName, string value)> Properties { get; set; }
        public List<string> Images { get; set; }

    }
}
