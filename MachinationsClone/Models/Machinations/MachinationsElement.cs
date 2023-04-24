using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    public abstract class MachinationsElement
    {
        public Guid ElementId { get; private set; }
        public string Type { get; set; }

        public MachinationsGraph Graph { get; private set; }

        private List<MachinationsConnection> _inputs;
        private List<MachinationsConnection> _outputs;
        
        public IEnumerable<MachinationsConnection> Inputs => _inputs;
        public IEnumerable<MachinationsConnection> Outputs => _outputs;

        protected MachinationsElement(Guid elementId, MachinationsGraph graph)
        {
            ElementId = elementId;
            Graph = graph;
            _inputs = new List<MachinationsConnection>();
            _outputs = new List<MachinationsConnection>();
        }

        public abstract void InitializeState();
        public abstract void LoadState(Dictionary<string, string> state);
        public abstract Dictionary<string, string> GetState();
        
        public void AddInput(MachinationsConnection input)
        {
            _inputs.Add(input);
        }
        
        public void AddOutput(MachinationsConnection output)
        {
            _outputs.Add(output);
        }

        public abstract void Trigger();
    }
}