using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations.Nodes
{
    public class MachinationsPool : MachinationsNode
    {
        // Properties
        public int Capacity { get; set; }
        public int StartAmount { get; set; }
        
        // State
        public int CurrentAmount { get; set; }

        public MachinationsPool(Guid elementId, MachinationsGraph graph) : base(elementId, graph)
        {
        }
        
        public static Dictionary<string, string> GetDefaultProperties()
        {
            return new Dictionary<string, string>
            {
                {"capacity", "-1"},
                {"startAmount", "0"},
            };
        }

        public static MachinationsPool Create(MachinationsGraph graph, GraphNode node)
        {
            var pool = new MachinationsPool(node.Id, graph)
            {
                Capacity = int.Parse(node.Properties.GetValueOrDefault("capacity", "-1")),
                StartAmount = int.Parse(node.Properties.GetValueOrDefault("startAmount", "0")),
            };
            
            return pool;
        }

        public override void InitializeState()
        {
            CurrentAmount = StartAmount;
        }

        public override void LoadState(Dictionary<string, string> state)
        {
            if (state.TryGetValue("currentAmount", out var currentAmount))
            {
                CurrentAmount = int.Parse(currentAmount);
            }
        }

        public override Dictionary<string, string> GetState()
        {
            var state = new Dictionary<string, string>
            {
                {"currentAmount", CurrentAmount.ToString()}
            };
            
            return state;
        }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }
    }
}