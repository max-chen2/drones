using System.Collections.Generic;

namespace Drone
{
    public class AssignmentOutputDto
    {
        public int DroneId { get; set; }
        public int PackageId { get; set; }
    }
    public class OutputModel
    {
        public List<AssignmentOutputDto> Assignments { get; set; }
        public List<int> UnassignedPackageIds { get; set; }
    } 
}
