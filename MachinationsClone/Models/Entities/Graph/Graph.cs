using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinationsClone.Models.Entities.Graph
{
    public class Graph
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
        public List<GraphElement> GraphElements { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public int StepSize { get; set; } = 1;

        public Guid? CurrentStatesGroupId { get; set; } = null;
        [ForeignKey("CurrentStatesGroupId")]
        public GraphStatesGroup CurrentStatesGroup { get; set; } = null;
        
        public Guid? CurrentStateId { get; set; } = null;
        [ForeignKey("CurrentStateId")]
        public GraphState CurrentState { get; set; } = null;
    }
}