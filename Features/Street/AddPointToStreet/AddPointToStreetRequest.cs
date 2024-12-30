using MediatR;
using NetTopologySuite.Geometries;

namespace StreetService.Features.Street.AddPointToStreet
{
    public record AddPointToStreetRequest(int StreetId, Point Point) : IRequest<AddPointToStreetResponse>;
}
