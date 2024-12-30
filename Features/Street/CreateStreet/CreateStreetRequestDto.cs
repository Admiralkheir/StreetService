using NetTopologySuite.Geometries;

namespace StreetService.Features.Street.CreateStreet
{
    public record CreateStreetRequestDto(string StreetName, int Capacity, LineString Geometry);
}
