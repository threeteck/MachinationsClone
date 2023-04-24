using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinationsClone.Models.Entities.Graph
{
    public class GraphState
    {
        public Guid Id { get; set; }

        public Guid GraphId { get; set; }
        [ForeignKey("GraphId")]
        public Graph Graph { get; set; }
        
        public Guid? GraphStatesGroupId { get; set; } = null;
        [ForeignKey("GraphStatesGroupId")]
        public GraphStatesGroup GraphStatesGroup { get; set; } = null;
        
        public int Step { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<Guid, GraphElementState> GraphElementStates { get; set; }
    }
    
    public class GraphElementState
    {
        public Guid GraphElementId { get; set; }
        public string GraphElementType { get; set; }
        
        public Dictionary<string, string> ElementState { get; set; }
    }
}