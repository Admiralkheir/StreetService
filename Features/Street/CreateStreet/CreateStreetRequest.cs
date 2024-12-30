using MediatR;
using NetTopologySuite.Geometries;

namespace StreetService.Features.Street.CreateStreet
{
    public record CreateStreetRequest(string StreetName, int Capacity, LineString Geometry) : IRequest<CreateStreetResponse>;
}
