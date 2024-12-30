using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StreetService.Data;
using StreetService.Exceptions;

namespace StreetService.Features.Street.DeleteStreet
{
    public class DeleteStreetRequestHandler : IRequestHandler<DeleteStreetRequest, DeleteStreetResponse>
    {
        private readonly StreetDbContext _context;
        public DeleteStreetRequestHandler(StreetDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteStreetResponse> Handle(DeleteStreetRequest request, CancellationToken cancellationToken)
        {
            var street = await _context.Street.FirstOrDefaultAsync(x=>x.Id == request.StreetId && x.IsDeleted == false,cancellationToken);

            if (street is null)
            {
                throw new StreetNotFoundException(request.StreetId);
            }

            street.IsDeleted = true;

            _context.Street.Update(street);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteStreetResponse(street.Id,street.Name);
        }
    }
}
