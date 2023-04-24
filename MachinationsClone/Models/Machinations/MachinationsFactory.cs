using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    public delegate MachinationsNode MachinationsNodeFactory(MachinationsGraph graph, GraphNode graphNode);
    public delegate MachinationsConnection MachinationsConnectionFactory(MachinationsGraph graph, GraphConnection graphConnection);
    
    public static class MachinationsFactory
    {
        private static Dictionary<string, MachinationsNodeFactory> _nodeFactories = new Dictionary<string, MachinationsNodeFactory>();
        private static Dictionary<string, MachinationsConnectionFactory> _connectionFactories = new Dictionary<string, MachinationsConnectionFactory>();
        
        private static Dictionary<string, Dictionary<string, string>> _elementsDefaultProperties = new Dictionary<string, Dictionary<string, string>>();

        public static void RegisterNode(string name, MachinationsNodeFactory factory, Dictionary<string, string> defaultProperties)
        {
            _nodeFactories.Add(name, factory);
            _elementsDefaultProperties.Add(name, defaultProperties);
        }

        public static void RegisterConnection(string name, MachinationsConnectionFactory factory, Dictionary<string, string> defaultProperties)
        {
            _connectionFactories.Add(name, factory);
            _elementsDefaultProperties.Add(name, defaultProperties);
        }

        public static MachinationsNode CreateNode(MachinationsGraph graph, GraphNode graphNode)
        {
            if (!_nodeFactories.ContainsKey(graphNode.NodeTypeName))
            {
                throw new KeyNotFoundException($"Node with type name {graphNode.NodeTypeName} not found.");
            }
            
            var node = _nodeFactories[graphNode.NodeTypeName].Invoke(graph, graphNode);
            node.Type = graphNode.NodeTypeName;
            node.Name = graphNode.Name;
            node.ActivationMode = ActivationMode.GetFromEnum(graphNode.ActivationMode);
            node.PullMode = PullMode.GetFromEnum(graphNode.PullMode);
            return node;
        }
        
        public static MachinationsConnection CreateConnection(MachinationsGraph graph, GraphConnection graphConnection)
        {
            if (!_connectionFactories.ContainsKey(graphConnection.ConnectionTypeName))
            {
                throw new KeyNotFoundException($"Connection with type name {graphConnection.ConnectionTypeName} not found.");
            }
            
            var connection = _connectionFactories[graphConnection.ConnectionTypeName].Invoke(graph, graphConnection);
            connection.Type = graphConnection.ConnectionTypeName;
            connection.StartId = graphConnection.StartId ?? Guid.Empty;
            connection.EndId = graphConnection.EndId ?? Guid.Empty;
            return connection;
        }
        
        public static MachinationsElement CreateElement(MachinationsGraph graph, GraphElement graphElement)
        {
            if (graphElement is GraphNode graphNode)
            {
                return CreateNode(graph, graphNode);
            }
            else if (graphElement is GraphConnection graphConnection)
            {
                return CreateConnection(graph, graphConnection);
            }
            else
            {
                throw new System.Exception("Unknown graph element type.");
            }
        }
        
        public static Dictionary<string, string> GetDefaultProperties(string type)
        {
            if (!_elementsDefaultProperties.ContainsKey(type))
            {
                throw new KeyNotFoundException($"Element with type name {type} not found.");
            }

            return _elementsDefaultProperties[type];
        }
    }
}