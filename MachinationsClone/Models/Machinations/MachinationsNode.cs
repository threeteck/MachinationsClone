using System;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    public abstract class MachinationsNode : MachinationsElement
    {
        public string Name { get; set; }

        // Node properties
        public string ActivationMode { get; set; }
        public string PullMode { get; set; }

        protected MachinationsNode(Guid elementId, MachinationsGraph graph) : base(elementId, graph)
        {
        }
    }
}