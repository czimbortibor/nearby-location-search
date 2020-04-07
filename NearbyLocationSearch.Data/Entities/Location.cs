using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace NearbyLocationSearch.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }
        
        [Required]
        public Point Coordinates { get; set; }
    }
}