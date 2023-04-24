using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;
using MachinationsClone.Models.Machinations;
using MachinationsClone.Models.Machinations.Connections;
using MachinationsClone.Models.Machinations.Nodes;

namespace MachinationsClone
{
    public static class GraphGrammar
    {
        private static List<NodeType> _nodeTypes;
        private static List<ConnectionType> _connectionTypes;
        
        public static IEnumerable<NodeType> NodeTypes => _nodeTypes;
        public static IEnumerable<ConnectionType> ConnectionTypes => _connectionTypes;
        
        private static bool _initialized = false;

        public static void InitializeGraphGrammar()
        {
            if (_initialized) return;
            
            _nodeTypes = new List<NodeType>();
            _connectionTypes = new List<ConnectionType>();
            
            AddNodeType("pool",
                "A pool is a named node, that abstracts from an in-game entity, and can contain resources, such as coins, crystals, health, etc.",
                "pool",
                true,
                MachinationsPool.Create,
                MachinationsPool.GetDefaultProperties());
            
            AddNodeType("source",
                "A source can be thought of as a pool with an infinite amount of resources, and therefore always pushes all resources or all resources are pulled from it.",
                "source",
                false,
                MachinationsSource.Create,
                MachinationsSource.GetDefaultProperties());

            AddConnectionType("resourceConnection",
                "A resource connection is an edge with an associated expression that defines the rate at which resources can flow between source and target nodes.",
                LineType.Solid,
                MachinationsResourceConnection.Create,
                MachinationsResourceConnection.GetDefaultProperties());
            
            _initialized = true;
        }
        
        public static void AddNodeType(string name, string description, string symbol, bool exportable, MachinationsNodeFactory factory, Dictionary<string, string> defaultProperties)
        {
            _nodeTypes.Add(new NodeType(name, description, symbol, exportable));
            MachinationsFactory.RegisterNode(name, factory, defaultProperties);
        }
        
        public static void AddConnectionType(string name, string description, LineType lineType, MachinationsConnectionFactory factory, Dictionary<string, string> defaultProperties)
        {
            _connectionTypes.Add(new ConnectionType(name, description, lineType));
            MachinationsFactory.RegisterConnection(name, factory, defaultProperties);
        }
    }
}