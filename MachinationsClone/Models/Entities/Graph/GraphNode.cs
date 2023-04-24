using System;
using System.ComponentModel.DataAnnotations.Schema;
using MachinationsClone.Models.Entities.Geometry;

namespace MachinationsClone.Models.Entities.Graph
{
    public enum ActivationModeEnum
    {
        Auto = 0,
        Passive = 1,
        OnStart = 2,
    }
    
    public enum PullModeEnum
    {
        PullAll = 0,
        PullAny = 1,
        PushAll = 2,
        PushAny = 3,
    }

    public class GraphNode : GraphElement
    {
        public string Name { get; set; }
        public ActivationModeEnum ActivationMode { get; set; }
        public PullModeEnum PullMode { get; set; }
        
        [ForeignKey("NodeTypeName")]
        public NodeType NodeType { get; set; }
        public string NodeTypeName { get; set; }
        
        public NodeGeometry NodeGeometry { get; set; }
    }
}