using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite;
using NearbyLocationSearch.Service.Models;
using NearbyLocationSearch.Data.Repositories;
using NearbyLocationSearch.Data.Entities;
using NearbyLocationSearch.Data.Models;

namespace NearbyLocationSearch.Service
{
    public interface ILocationService
    {
        Task<SearchResult> GetLocations(int maxResults);
        Task<SearchResult> GetLocations(LocationModel location, int maxDistance, int maxResults);
    }

    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        
        public LocationService(ILocationRepository repository)
        {
            _repository = repository;
        }


        public async Task<SearchResult> GetLocations(int maxResults)
        {
            var locations = await ReadLocations(maxResults);
            var searchResults = ProjectToSearchResults(locations);

            return searchResults;
        }

        private async Task<IEnumerable<Location>> ReadLocations(int maxResults)
        {
            var locations = await _repository.GetLocations(maxResults);
            return locations;
        }

        private SearchResult ProjectToSearchResults(IEnumerable<Location> locations)
        {
            return new SearchResult
            {
                Locations = locations.Select(x => new RegisteredLocationModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    Latitude = x.Coordinates.Coordinate.Y,
                    Longitude = x.Coordinates.Coordinate.X
                })
            };
        }


        public async Task<SearchResult> GetLocations(LocationModel location, int maxDistance, int maxResults)
        {
            var targetPoint = ConvertToPoint(location);
            var nearbyLocations = await ReadNearbyLocations(targetPoint, maxDistance, maxResults);

            var searchResults = ProjectToSearchResults(nearbyLocations);
            return searchResults;
        }

        private async Task<IEnumerable<NearbyLocationModel>> ReadNearbyLocations(NetTopologySuite.Geometries.Point targetPoint, int maxDistance, int maxResults)
        {
            var locations = await _repository.GetLocations(targetPoint, maxDistance, maxResults);
            return locations;
        }

        private NetTopologySuite.Geometries.Point ConvertToPoint(LocationModel location)
        {
            _ = location ?? throw new ArgumentNullException(nameof(location));


            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var coordinates = new NetTopologySuite.Geometries.Coordinate(location.Longitude, location.Latitude);
            var point = geometryFactory.CreatePoint(coordinates);

            return point;
        }

        private SearchResult ProjectToSearchResults(IEnumerable<NearbyLocationModel> nearbyLocations)
        {
            return new SearchResult
            {
                Locations = nearbyLocations.Select(x => new RegisteredLocationModel
                {
                    Id = x.Location.Id,
                    Address = x.Location.Address,
                    Latitude = x.Location.Coordinates.Coordinate.Y,
                    Longitude = x.Location.Coordinates.Coordinate.X,
                    DistanceInMeters = x.DistanceInMeters
                })
            };
        }
    }
}