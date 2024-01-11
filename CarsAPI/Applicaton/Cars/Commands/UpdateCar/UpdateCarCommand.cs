using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shared.Dto;

namespace Applicaton.Cars.Commands.UpdateCar
{
    public class UpdateCarCommand : IRequest
    {
        public Guid Id { get; set; }
        public CarFullInfoDtoV2 CarInfo { get; set; }
    }
}
