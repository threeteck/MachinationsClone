using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinationsClone.Models.Entities.Graph
{
    public class GraphElement
    {
        public Guid Id { get; set; }
        
        public Guid GraphId { get; set; }
        [ForeignKey("GraphId")]
        public Graph Graph { get; set; }

        [Column(TypeName = "jsonb")]
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}