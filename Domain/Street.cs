using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NetTopologySuite.Geometries;

namespace StreetService.Domain
{
    public class Street : IEntity<int>
    {
        public int Id { get; }
        public string Name { get; set; }
        public LineString Geometry { get; set; }
        public int Capacity { get; set; }
    }
}
