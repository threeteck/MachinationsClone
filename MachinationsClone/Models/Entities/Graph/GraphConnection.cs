using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinationsClone.Models.Entities.Graph
{
    public class GraphConnection : GraphElement
    {
        [ForeignKey("ConnectionTypeName")]
        public ConnectionType ConnectionType { get; set; }
        public string ConnectionTypeName { get; set; }

        public Guid? StartId { get; set; } = null;
        [ForeignKey("StartId")]
        public GraphElement Start { get; set; } = null;
        
        public Guid? EndId { get; set; } = null;
        [ForeignKey("EndId")]
        public GraphElement End { get; set; } = null;
    }
}