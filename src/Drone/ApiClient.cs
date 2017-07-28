using System.Collections.Generic;
using RestSharp;

namespace Drone
{
    public class ApiClient
    {
        public List<Drone> GetDrones()
        {
            RestClient c = new RestClient("https://codetest.kube.getswift.co");
            var request = new RestRequest("drones", Method.GET);
            var drones = c.Execute<List<Drone>>(request).Data;

            return drones;
        }

        public List<Package> GetPackages()
        {
            RestClient c = new RestClient("https://codetest.kube.getswift.co");
            var packageRequest = new RestRequest("packages", Method.GET);
            var packages = c.Execute<List<Package>>(packageRequest).Data;
            return packages;
        }
    }
}
