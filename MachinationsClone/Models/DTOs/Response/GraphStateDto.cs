using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.DTOs.Response
{
    public class GraphStateDto
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public Guid GraphId { get; set; }
        public Guid GraphStatesGroupId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public Dictionary<Guid, GraphElementStateDto> GraphElementStates { get; set; }
        
        public static GraphStateDto FromGraphState(GraphState graphState)
        {
            if (graphState == null)
            {
                return null;
            }
            
            var dto = new GraphStateDto
            {
                Id = graphState.Id,
                Step = graphState.Step,
                GraphId = graphState.GraphId,
                GraphStatesGroupId = graphState.GraphStatesGroupId ?? Guid.Empty,
                CreatedAt = graphState.CreatedAt,
            };
            
            dto.GraphElementStates = new Dictionary<Guid, GraphElementStateDto>();
            foreach (var (key, value) in graphState.GraphElementStates)
            {
                dto.GraphElementStates.Add(key, new GraphElementStateDto
                {
                    GraphElementId = value.GraphElementId,
                    GraphElementType = value.GraphElementType,
                    ElementState = value.ElementState
                });
            }
            
            return dto;
        }
    }
    
    public class GraphElementStateDto
    {
        public Guid GraphElementId { get; set; }
        public string GraphElementType { get; set; }
        
        public Dictionary<string, string> ElementState { get; set; }
    }   
}