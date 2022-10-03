using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Applicaton.Cars.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequest
    {
        public Guid Id { get; set; }
    }

}
