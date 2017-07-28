using System.Linq;

namespace Drone
{
    public class Distance
    {
        public static double DistanceToDestination(Package p, Location depotLocation)
        {
            double distance = depotLocation.Coordinate.GetDistanceTo(p.Destination.Coordinate);
            return distance;
        }

        public static double DistanceToDepot(Drone drone, Location depotLocation)
        {
            double distance = 0;
            if (drone.Packages.Any())
            {
                var deliverTo = drone.Packages[0]
                    .Destination.Coordinate;
                //Deliver first
                distance += drone.Location.Coordinate.GetDistanceTo(deliverTo);
                //Collect from depot
                distance += deliverTo.GetDistanceTo(depotLocation.Coordinate);
            }
            else
            {
                //Collect from depot
                distance += drone.Location.Coordinate.GetDistanceTo(depotLocation.Coordinate);
            }
            return distance;
        }
    }
}
