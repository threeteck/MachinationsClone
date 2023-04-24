using System;
using System.Collections.Generic;
using System.Linq;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    /// <summary>
    /// MachinationsGraph is a class that represents a Machinations graph.
    /// MachinationGraph in not an entity and is not stored in the database.
    /// It is compiled from entities in the database and consists of MachinationsNodes and MachinationsConnections.
    /// </summary>
    public class MachinationsGraph
    {
        public Guid GraphId { get; private set; }

        public int ElementCount => _elements.Count;
        public int NodeCount => _nodes.Count;
        public int ConnectionCount => _connections.Count;
        
        private List<MachinationsElement> _elements;
        private List<MachinationsNode> _nodes;
        private List<MachinationsConnection> _connections;
        
        private Dictionary<Guid, MachinationsElement> _elementsById;
        private Dictionary<Guid, MachinationsNode> _nodesById;
        private Dictionary<Guid, MachinationsConnection> _connectionsById;
        private Dictionary<string, MachinationsNode> _nodesByName;
        private bool _reverseOrder = false;

        public MachinationsGraph(Guid graphId)
        {
            GraphId = graphId;
            _elements = new List<MachinationsElement>();
            _nodes = new List<MachinationsNode>();
            _connections = new List<MachinationsConnection>();
            _elementsById = new Dictionary<Guid, MachinationsElement>();
            _nodesById = new Dictionary<Guid, MachinationsNode>();
            _connectionsById = new Dictionary<Guid, MachinationsConnection>();
            _nodesByName = new Dictionary<string, MachinationsNode>();
        }
        
        public void CompileGraph(List<GraphElement> graphElements)
        {
            foreach (var graphElement in graphElements)
            {
                var machinationsElement = MachinationsFactory.CreateElement(this, graphElement);
                _elements.Add(machinationsElement);
                _elementsById.Add(machinationsElement.ElementId, machinationsElement);
                if (machinationsElement is MachinationsNode machinationsNode)
                {
                    _nodes.Add(machinationsNode);
                    _nodesById.Add(machinationsNode.ElementId, machinationsNode);
                    _nodesByName.Add(machinationsNode.Name, machinationsNode);
                }
                else if (machinationsElement is MachinationsConnection machinationsConnection)
                {
                    _connections.Add(machinationsConnection);
                    _connectionsById.Add(machinationsConnection.ElementId, machinationsConnection);
                }
            }
            
            // Now that all elements have been created, we can connect them.
            foreach (var machinationsConnection in _connections)
            {
                if (machinationsConnection.StartId != Guid.Empty && machinationsConnection.EndId != Guid.Empty)
                {
                    machinationsConnection.SetStart(_elementsById[machinationsConnection.StartId]);
                    machinationsConnection.SetEnd(_elementsById[machinationsConnection.EndId]);
                }
                
                if (machinationsConnection.Start is not null && machinationsConnection.End is not null)
                {
                    machinationsConnection.Start.AddOutput(machinationsConnection);
                    machinationsConnection.End.AddInput(machinationsConnection);
                }
            }
        }

        public MachinationsElement GetElement(Guid elementId)
        {
            if (_elementsById.TryGetValue(elementId, out var element))
            {
                return element;
            }
            return null;
        }
        
        public MachinationsNode GetNode(Guid nodeId)
        {
            if (_nodesById.TryGetValue(nodeId, out var node))
            {
                return node;
            }
            return null;
        }
        
        public MachinationsNode GetNode(string nodeName)
        {
            if (_nodesByName.TryGetValue(nodeName, out var node))
            {
                return node;
            }
            return null;
        }
        
        public MachinationsConnection GetConnection(Guid connectionId)
        {
            if (_connectionsById.TryGetValue(connectionId, out var connection))
            {
                return connection;
            }
            return null;
        }
        
        public void InitializeState()
        {
            foreach (var machinationsElement in _elements)
            {
                machinationsElement.InitializeState();
            }
        }

        public void LoadState(GraphState state)
        {
            InitializeState();
            
            foreach (var (elementId, elementState) in state.GraphElementStates)
            {
                if (_elementsById.TryGetValue(elementId, out var machinationsElement))
                {
                    machinationsElement.LoadState(elementState.ElementState);
                }
            }
        }

        public GraphState GetState(DateTime timestamp)
        {
            var graphState = new GraphState
            {
                GraphId = GraphId,
                CreatedAt = timestamp,
                GraphElementStates = new Dictionary<Guid, GraphElementState>()
            };
            
            foreach (var machinationsElement in _elements)
            {
                var elementStateDict = machinationsElement.GetState();
                if (elementStateDict is null || elementStateDict.Count == 0)
                {
                    continue;
                }
                
                var elementState = new GraphElementState
                {
                    GraphElementId = machinationsElement.ElementId,
                    GraphElementType = machinationsElement.Type,
                    ElementState = elementStateDict
                };
                
                graphState.GraphElementStates.Add(elementState.GraphElementId, elementState);
            }
            
            return graphState;
        }

        public void Run(int currentStep)
        {
            /*if (currentStep == 0)
            {
                foreach (var node in _nodes.Where(n => n.ActivationMode == ActivationMode.ON_START))
                {
                    node.Trigger();
                }
            }

            var auto = _nodes.Where(n => n.ActivationMode == ActivationMode.AUTO);
            if (_reverseOrder)
                auto = auto.Reverse();
            _reverseOrder = !_reverseOrder;
            foreach (var node in auto)
            {
                node.Trigger();
            }*/
        }
    }
        
}