using System.Collections.Generic;

namespace NearbyLocationSearch.Service.Models
{
    public class SearchResult
    {
        public IEnumerable<RegisteredLocationModel> Locations { get; set; }
    }
}