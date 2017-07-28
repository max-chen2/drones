using System;
using System.Device.Location;

namespace Drone
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public GeoCoordinate Coordinate => new GeoCoordinate(Latitude, Longitude);
    }
}
