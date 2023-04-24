using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.DTOs.Response
{
    public class GraphDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StepSize { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public Guid? CurrentStatesGroupId { get; set; }
        public GraphStatesGroupDto CurrentStatesGroup { get; set; }
        
        public Guid? CurrentStateId { get; set; }
        public GraphStateDto CurrentState { get; set; }
        
        public Guid UserId { get; set; }
        
        public List<GraphElementDto> GraphElements { get; set; }
        
        public static GraphDto FromGraph(Graph graph)
        {
            if (graph == null)
            {
                return null;
            }
            
            return new GraphDto
            {
                Id = graph.Id,
                Name = graph.Name,
                Description = graph.Description,
                StepSize = graph.StepSize,
                CreatedAt = graph.CreatedAt,
                UpdatedAt = graph.UpdatedAt,
                CurrentStateId = graph.CurrentStateId,
                CurrentState = GraphStateDto.FromGraphState(graph.CurrentState),
                CurrentStatesGroupId = graph.CurrentStatesGroupId,
                CurrentStatesGroup = GraphStatesGroupDto.FromGraphStatesGroup(graph.CurrentStatesGroup),
                UserId = graph.UserId,
                GraphElements = GraphElementDto.FromGraphElements(graph.GraphElements)
            };
        }
    }
}