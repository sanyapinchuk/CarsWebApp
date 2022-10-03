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
        private readonly IDataContext _dataContext;
        public DeleteCarCommandHadnler(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<Unit> Handle(DeleteCarCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = await _dataContext.Cars.FindAsync(new Car() { Id = request.Id });
            if (entity == null)
                throw new EntityNotFoundException(nameof(Car),request.Id);

            _dataContext.Cars.Remove(entity);
            await _dataContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
