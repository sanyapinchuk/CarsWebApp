using MediatR;
namespace Applicaton.Cars.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequest
    {
        public Guid Id { get; set; }
    }

}
