namespace NearbyLocationSearch.Api.Models
{
    public class RegisteredLocationModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? DistanceInMeters { get; set; }


        public override string ToString() => $"{Latitude}, {Longitude} - {Address}";
    }
}