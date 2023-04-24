using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations.Nodes
{
    public class MachinationsSource : MachinationsNode
    {
        public MachinationsSource(Guid elementId, MachinationsGraph graph) : base(elementId, graph)
        {
        }
        
        public static Dictionary<string, string> GetDefaultProperties()
        {
            return new Dictionary<string, string>();
        }

        public static MachinationsSource Create(MachinationsGraph graph, GraphNode node)
        {
            var source = new MachinationsSource(node.Id, graph);
            
            return source;
        }

        public override void InitializeState()
        {
        }

        public override void LoadState(Dictionary<string, string> state)
        {
        }

        public override Dictionary<string, string> GetState()
        {
            return null;
        }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }
    }
}