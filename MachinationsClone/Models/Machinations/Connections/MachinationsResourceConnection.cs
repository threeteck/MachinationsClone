using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations.Connections
{
    public class MachinationsResourceConnection : MachinationsConnection
    {
        // Properties
        public int Speed { get; set; }
        
        public MachinationsResourceConnection(Guid elementId, MachinationsGraph graph) : base(elementId, graph)
        {
        }
        
        public static Dictionary<string, string> GetDefaultProperties()
        {
            return new Dictionary<string, string>
            {
                {"speed", "1"},
            };
        }
        
        public static MachinationsResourceConnection Create(MachinationsGraph graph, GraphConnection connection)
        {
            var resourceConnection = new MachinationsResourceConnection(connection.Id, graph)
            {
                Speed = int.Parse(connection.Properties.GetValueOrDefault("speed", "1")),
            };
            
            return resourceConnection;
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