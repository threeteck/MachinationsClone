using System;

namespace MachinationsClone.Models.DTOs.Request
{
    public class UpdateNodeGeometryDto
    {
        public Guid NodeId { get; set; }
        public double? X { get; set; } = null;
        public double? Y { get; set; } = null;
    }
}