using System;
using System.Collections.Generic;
using System.Linq;

namespace Drone
{
    public class Auction
    {
        private readonly Location _depotLocation;
        private readonly double _speedKmPerHour;
        public Auction(Location depotLocation, double speedKmPerHour)
        {
            _depotLocation = depotLocation;
            _speedKmPerHour = speedKmPerHour;
        }

        private void CalculateDistanceToDepot(List<Drone> ds)
        {
            foreach (var d in ds)
            {
                d.TotalDistanceToDepot = Distance.DistanceToDepot(d, _depotLocation);
            }
        }

        private void CalculateDistanceToDestination(List<Package> ps)
        {
            foreach (var p in ps)
            {
                p.DistanceToDestination = Distance.DistanceToDestination(p, _depotLocation);
                double nowSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
                double secondsLeft = p.Deadline - nowSeconds;
                p.Difficulty = p.DistanceToDestination / secondsLeft;
            }
        }


        public AuctionResult StartAuction(List<Drone> ds, List<Package> ps)
        {
            CalculateDistanceToDepot(ds);
            CalculateDistanceToDestination(ps);
            List<Assignment> assignments = new List<Assignment>();
            List<Package> unassignedPackages = new List<Package>();
            var sortedPackagesByDifficulty = ps.OrderByDescending(t => t.Difficulty).ToList();
            var sortedDronesByDistanceToDepot = ds.OrderBy(t => t.TotalDistanceToDepot).ToList();
            //START from HARDEST, the HARDEST package tries the best drone, if cannot be delivered before deadline, exit and try the next hardest
            foreach (var p in sortedPackagesByDifficulty)
            {
                int index = sortedPackagesByDifficulty.IndexOf(p);
                //The best available drone (the one closest to depot)
                var winner = sortedDronesByDistanceToDepot.FirstOrDefault();
                if (winner != null)
                {
                    double totalDistance = winner.TotalDistanceToDepot.Value + p.DistanceToDestination.Value;
                    bool can = DeadlineUtil.CanDeliverBeforeDeadline(totalDistance, p.Deadline, _speedKmPerHour);
                    if (can)
                    {
                        assignments.Add(new Assignment
                        {
                            Drone = winner,
                            Package = p,
                        });
                        sortedDronesByDistanceToDepot.Remove(winner);
                        continue;
                    }
                }
                unassignedPackages.Add(p);
            }
            return new AuctionResult
            {
                Assignments = assignments,
                UnassignedPackages = unassignedPackages,
            };
        }
    }
}
