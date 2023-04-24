using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinationsClone.Models.Entities.Graph
{
    public class GraphStatesGroup
    {
        public Guid Id { get; set; }
        
        public int Order { get; set; }
        
        public Guid GraphId { get; set; }
        [ForeignKey("GraphId")]
        public Graph Graph { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public int TotalSteps { get; set; }

        public Guid? LastStateId { get; set; } = null;
        [ForeignKey("LastStateId")]
        public GraphState LastState { get; set; } = null;
    }
}