using System;
using MachinationsClone.Models.Entities.Geometry;

namespace MachinationsClone.Models.DTOs.Response
{
    public class NodeGeometryDto
    {
        public Guid Id { get; set; }
        public Guid NodeId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        
        public static NodeGeometryDto FromNodeGeometry(NodeGeometry nodeGeometry)
        {
            if (nodeGeometry == null)
            {
                return null;
            }
            
            return new NodeGeometryDto
            {
                X = nodeGeometry.X,
                Y = nodeGeometry.Y,
                Id = nodeGeometry.Id,
                NodeId = nodeGeometry.GraphNodeId
            };
        }
    }
}