using System;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    public abstract class MachinationsConnection : MachinationsElement
    {
        public Guid StartId { get; set; }
        public MachinationsElement Start { get; private set; }
        
        public Guid EndId { get; set; }
        public MachinationsElement End { get; private set; }

        protected MachinationsConnection(Guid elementId, MachinationsGraph graph) : base(elementId, graph)
        {
        }
        
        public void SetStart(MachinationsElement start)
        {
            Start = start;
            StartId = start.ElementId;
        }
        
        public void SetEnd(MachinationsElement end)
        {
            End = end;
            EndId = end.ElementId;
        }
    }
}