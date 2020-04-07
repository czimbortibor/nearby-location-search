using NearbyLocationSearch.Data.Entities;

namespace NearbyLocationSearch.Data.Models
{
    public class NearbyLocationModel
    {
        public Location Location { get; set; }
        public int DistanceInMeters { get; set; }
    }
}