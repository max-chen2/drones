using System.Collections.Generic;

namespace Drone
{
    public class Drone
    {
        public int DroneId { get; set; }
        public Location Location { get; set; }
        public List<Package> Packages { get; set; }
        public double? TotalDistanceToDepot { get; set; }
    }
}