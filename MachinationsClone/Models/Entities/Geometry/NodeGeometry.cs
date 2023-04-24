using System;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Entities.Geometry
{
    public class NodeGeometry
    {
        public Guid Id { get; set; }
        
        public Guid GraphNodeId { get; set; }
        public GraphNode GraphNode { get; set; }
        
        public double X { get; set; }
        public double Y { get; set; }
    }
}