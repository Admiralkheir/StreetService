using NetTopologySuite.Geometries;

namespace StreetService.Features.Street.GetStreet
{
    public record GetStreetResponse(int Id, string StreetName, int Capacity, LineString Geometry);
}
