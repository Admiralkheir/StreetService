using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NetTopologySuite.Geometries;

namespace StreetService.Domain.Entities
{
    public class Street : IEntity<int>
    {
        public int Id { get; }
        public string Name { get; set; }
        public LineString Geometry { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; }
        public bool IsDeleted { get; set; }
    }
}
