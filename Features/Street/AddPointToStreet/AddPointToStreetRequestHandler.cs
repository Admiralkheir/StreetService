using Medallion.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;
using StreetService.Data;
using StreetService.Exceptions;

namespace StreetService.Features.Street.AddPointToStreet
{
    public class AddPointToStreetRequestHandler : IRequestHandler<AddPointToStreetRequest, AddPointToStreetResponse>
    {
        private readonly StreetDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IDistributedLockProvider _distributedLockProvider;
        public AddPointToStreetRequestHandler(StreetDbContext context, IConfiguration configuration, IDistributedLockProvider distributedLockProvider)
        {
            _context = context;
            _configuration = configuration;
            _distributedLockProvider = distributedLockProvider;
        }

        // After adding each point maybe capacity should be calculated again due to that we would use distributed lock mechanism
        public async Task<AddPointToStreetResponse> Handle(AddPointToStreetRequest request, CancellationToken cancellationToken)
        {
            //Acquire distributed lock
            using (_distributedLockProvider.AcquireLock($"Street-{request.StreetId}", TimeSpan.FromSeconds(30), cancellationToken))
            {
                // check street is exist
                var street = await _context.Street.FirstOrDefaultAsync(x => x.Id == request.StreetId && x.IsDeleted == false, cancellationToken);

                if (street is null)
                    throw new StreetNotFoundException(request.StreetId);

                // Chose method, with DB or Algorithm
                var usePostGis = _configuration.GetValue<bool>("UsePostGis");

                if (usePostGis)
                {
                    // PostGIS operation
                    await _context.Database.ExecuteSqlRawAsync(
                        @"UPDATE ""Street"" 
                              SET ""Geometry"" = ST_AddPoint(""Geometry"", ST_GeomFromText({0},4326), CASE 
                                WHEN ST_Distance(ST_StartPoint(""Geometry""), ST_GeomFromText({0},4326)) < 
                                     ST_Distance(ST_EndPoint(""Geometry""), ST_GeomFromText({0},4326))
                                THEN 0 ELSE -1 END)
                              WHERE ""Id"" = {1}",
                        new object[] { request.Point.ToString(), request.StreetId }
                    );

                    // We would think for capacity calculation after adding Point

                }
                else
                {
                    // In-memory operation
                    var coordinates = street.Geometry.Coordinates.ToList();
                    var startDistance = request.Point.Distance(street.Geometry.StartPoint);
                    var endDistance = request.Point.Distance(street.Geometry.EndPoint);

                    if (startDistance < endDistance)
                    {
                        coordinates.Insert(0, request.Point.Coordinate);
                    }
                    else
                    {
                        coordinates.Add(request.Point.Coordinate);
                    }

                    street.Geometry = new LineString(coordinates.ToArray()) 
                    {
                        SRID=4326
                    };

                    _context.Update(street);
                    await _context.SaveChangesAsync();
                }

                return new AddPointToStreetResponse(street.Id, street.Geometry);

            }

        }
    }
}
