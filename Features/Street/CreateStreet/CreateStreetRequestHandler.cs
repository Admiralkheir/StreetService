using MediatR;
using StreetService.Data;
using StreetService.Domain.Entities;

namespace StreetService.Features.Street.CreateStreet
{
    public class CreateStreetRequestHandler : IRequestHandler<CreateStreetRequest, CreateStreetResponse>
    {
        private readonly StreetDbContext _context;
        public CreateStreetRequestHandler(StreetDbContext context)
        {
            _context = context;
        }
        public async Task<CreateStreetResponse> Handle(CreateStreetRequest request, CancellationToken cancellationToken)
        {
            var street = new StreetService.Domain.Entities.Street()
            {
                Capacity = request.Capacity,
                Name = request.StreetName,
                Geometry = request.Geometry
            };

            _context.Street.Add(street);
            await _context.SaveChangesAsync();

            return new CreateStreetResponse(street.Id);
        }
    }
}
