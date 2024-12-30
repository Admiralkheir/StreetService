using MediatR;
using Microsoft.EntityFrameworkCore;
using StreetService.Data;
using StreetService.Exceptions;

namespace StreetService.Features.Street.GetStreet
{
    public class GetStreetRequestHandler : IRequestHandler<GetStreetRequest, GetStreetResponse>
    {
        private readonly StreetDbContext _context;
        public GetStreetRequestHandler(StreetDbContext context)
        {
            _context = context;
        }
        public async Task<GetStreetResponse> Handle(GetStreetRequest request, CancellationToken cancellationToken)
        {
            var street = await _context.Street.FirstOrDefaultAsync(x => x.Id == request.StreetId && x.IsDeleted == false,cancellationToken);
            
            if (street is null)
            {
                throw new StreetNotFoundException(request.StreetId);
            }

            return new GetStreetResponse(street.Id,street.Name,street.Capacity,street.Geometry);
        }
    }
}
