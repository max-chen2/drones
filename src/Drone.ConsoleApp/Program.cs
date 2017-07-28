using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;

namespace Drone.ConsoleApp
{
    class Program
    {
        private static List<Drone> _drones;
        private static List<Package> _packages;
        private static Location _depotLocation;
        private static double _speed;
        static void Main(string[] args)
        {
            ApiClient api = new ApiClient();
            _drones = api.GetDrones();
            _packages = api.GetPackages();
            _speed = double.Parse(ConfigurationManager.AppSettings["DroneSpeed"]);

            //303 Collins Street, Melbourne, VIC 3000
            _depotLocation = new Location
            {
                Longitude = double.Parse(ConfigurationManager.AppSettings["DepotLongitude"]),
                Latitude = double.Parse(ConfigurationManager.AppSettings["DepotLatitude"]),
            };

            Auction auction = new Auction(_depotLocation, _speed);
            AuctionResult result = auction.StartAuction(_drones, _packages);

            OutputModel output = new OutputModel
            {
                Assignments = result.Assignments.Select(t => new AssignmentOutputDto {DroneId = t.Drone.DroneId, PackageId = t.Package.PackageId})
                    .ToList(),
                UnassignedPackageIds = result.UnassignedPackages.Select(t => t.PackageId)
                    .ToList(),
            };
            var str = JsonConvert.SerializeObject(output);
            Console.Out.WriteLine(str);
            //Console.Out.WriteLine($"Assigned {result.Assignments.Count} packages");
            //Console.Out.WriteLine($"{result.UnassignedPackages.Count} cannot be delivered");
            Console.ReadKey();
        }

        
        
    }
    
}
