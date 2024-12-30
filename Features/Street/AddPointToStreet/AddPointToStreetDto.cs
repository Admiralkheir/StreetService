using NetTopologySuite.Geometries;

namespace StreetService.Features.Street.AddPointToStreet
{
    public record AddPointToStreetDto(int StreetId, Point point);
}
