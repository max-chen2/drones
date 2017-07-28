using System.Collections.Generic;

namespace Drone
{
    public class AuctionResult
    {
        public List<Assignment> Assignments { get; set; }
        public List<Package> UnassignedPackages { get; set; }
    }
}
