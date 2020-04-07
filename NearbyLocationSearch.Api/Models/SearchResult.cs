using System.Collections.Generic;

namespace NearbyLocationSearch.Api.Models
{
    public class SearchResult
    {
        public IEnumerable<RegisteredLocationModel> Locations { get; set; }
    }
}