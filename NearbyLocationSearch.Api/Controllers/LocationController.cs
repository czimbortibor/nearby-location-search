using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NearbyLocationSearch.Api.Models;
using NearbyLocationSearch.Service;

namespace NearbyLocationSearch.Api.Controllers
{
    [ApiController]
    [Route("locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }


        [HttpGet, Route("{maxResults}")]
        public async Task<IActionResult> GetLocations(int maxResults)
        {
            var searchResult = await _service.GetLocations(maxResults);

            return Ok(searchResult);
        }

        [HttpGet, Route("latitude&longitude&maxDistance&maxResults")]
        public async Task<IActionResult> GetLocations(double latitude, double longitude, int maxDistance, int maxResults)
        {
            var location = GetLocationModel(latitude, longitude);

            var searchResult = 
                await _service.GetLocations(location, maxDistance, maxResults);

            return Ok(searchResult);
        }

        private Service.Models.LocationModel GetLocationModel(double latitude, double longitude) 
            => new Service.Models.LocationModel
            {
                Latitude = latitude,
                Longitude = longitude
            };
    }
}