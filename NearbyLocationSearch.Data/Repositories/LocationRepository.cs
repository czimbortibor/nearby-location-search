using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NearbyLocationSearch.Data.Entities;
using NearbyLocationSearch.Data.Models;

namespace NearbyLocationSearch.Data.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetLocations(int maxResults);
        Task<IEnumerable<NearbyLocationModel>> GetLocations(NetTopologySuite.Geometries.Point targetPoint, int maxDistance, int maxResults);
    }

    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Location>> GetLocations(int maxResults)
        {
            return await _dbContext.Locations.AsNoTracking().Take(maxResults).ToListAsync();
        }

        public async Task<IEnumerable<NearbyLocationModel>> GetLocations(NetTopologySuite.Geometries.Point targetPoint, int maxDistance, int maxResults)
        {
            IQueryable<Location> locations =
                _dbContext.Locations.AsNoTracking();

            var distance = (double)maxDistance;
            IQueryable<Location> nearbyLocations = 
                locations
                    .Where(x => x.Coordinates.IsWithinDistance(targetPoint, distance))
                    .OrderBy(x => x.Coordinates.Distance(targetPoint));

            nearbyLocations = 
                nearbyLocations.Take(maxResults);

            var nearbyLocationModels = 
                ProjectToNearbyLocationModel(nearbyLocations, targetPoint);

            return await nearbyLocationModels.ToListAsync();
        }

        private IQueryable<NearbyLocationModel> ProjectToNearbyLocationModel(IQueryable<Location> locations, NetTopologySuite.Geometries.Point targetPoint)
        {
            return locations.Select(x => new NearbyLocationModel 
            {
                Location = x,
                // not sure about the conversion
                DistanceInMeters = (int)x.Coordinates.Distance(targetPoint)
            });
        }
    }
}