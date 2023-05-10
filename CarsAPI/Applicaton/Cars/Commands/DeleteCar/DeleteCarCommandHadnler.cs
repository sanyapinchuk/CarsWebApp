using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Applicaton.Interfaces;
using Domain;
using Applicaton.Common.Exceptions;

namespace Applicaton.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandHadnler : IRequestHandler<DeleteCarCommand>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DeleteCarCommandHadnler(IDataContext dataContext, IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<Unit> Handle(DeleteCarCommand request,
            CancellationToken cancellationToken)
        {
            await _repositoryManager.CarRepository.Delete(request.Id);

            return Unit.Value;
        }
    }
}
