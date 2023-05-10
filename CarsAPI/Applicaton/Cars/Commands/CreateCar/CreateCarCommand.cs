using MediatR;
using Shared.Dto;

namespace Applicaton.Cars.Commands.CreateCar
{
    public class CreateCarCommand : IRequest<Guid>
    {
        public CarFullInfoDto? CreateCarDto { get; set; }
    }
}
