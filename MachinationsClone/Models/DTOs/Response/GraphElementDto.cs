using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Geometry;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.DTOs.Response
{
    public abstract class GraphElementDto
    {
        public Guid Id { get; set; }
        public Guid GraphId { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public string ElementType { get; set; }
        
        public static List<GraphElementDto> FromGraphElements(List<GraphElement> graphElements)
        {
            if (graphElements == null)
            {
                return null;
            }
            
            var graphElementDtos = new List<GraphElementDto>();
            
            foreach (var graphElement in graphElements)
            {
                if (graphElement is GraphNode graphNode)
                {
                    graphElementDtos.Add(GraphNodeDto.FromGraphNode(graphNode));
                }
                else if (graphElement is GraphConnection graphConnection)
                {
                    graphElementDtos.Add(GraphConnectionDto.FromGraphConnection(graphConnection));
                }
            }
            
            return graphElementDtos;
        }
    }

    public class GraphNodeDto : GraphElementDto
    {
        public string Name { get; set; }
        public ActivationModeEnum ActivationMode { get; set; }
        public PullModeEnum PullMode { get; set; }
        public string NodeType { get; set; }
        
        public NodeGeometryDto Geometry { get; set; }
        
        public static GraphNodeDto FromGraphNode(GraphNode graphNode, NodeGeometry nodeGeometry = null)
        {
            if (graphNode == null)
            {
                return null;
            }
            
            return new GraphNodeDto
            {
                Id = graphNode.Id,
                GraphId = graphNode.GraphId,
                Properties = graphNode.Properties,
                ElementType = "node",
                Name = graphNode.Name,
                ActivationMode = graphNode.ActivationMode,
                PullMode = graphNode.PullMode,
                NodeType = graphNode.NodeTypeName,
                Geometry = NodeGeometryDto.FromNodeGeometry(nodeGeometry)
            };
        }
    }

    public class GraphConnectionDto : GraphElementDto
    {
        public Guid? StartId { get; set; } = null;
        public Guid? EndId { get; set; } = null;
        public string ConnectionType { get; set; }
        
        public static GraphConnectionDto FromGraphConnection(GraphConnection graphConnection)
        {
            if (graphConnection == null)
            {
                return null;
            }
            
            return new GraphConnectionDto
            {
                Id = graphConnection.Id,
                GraphId = graphConnection.GraphId,
                Properties = graphConnection.Properties,
                ElementType = "connection",
                StartId = graphConnection.StartId,
                EndId = graphConnection.EndId,
                ConnectionType = graphConnection.ConnectionTypeName
            };
        }
    }
}