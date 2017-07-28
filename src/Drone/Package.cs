namespace Drone
{
    public class Package
    {
        public int PackageId { get; set; }
        public long Deadline { get; set; }
        public Location Destination { get; set; }
        public double? DistanceToDestination { get; set; }
        public double? Difficulty { get; set; }
    }
}