using NetTopologySuite.Geometries;

namespace StreetService.Features.Street.AddPointToStreet
{
    public record AddPointToStreetResponse(int StreetId, LineString Geometry);
}
