namespace NearbyLocationSearch.Service.Models
{
    public class RegisteredLocationModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? DistanceInMeters { get; set; }
    }
}