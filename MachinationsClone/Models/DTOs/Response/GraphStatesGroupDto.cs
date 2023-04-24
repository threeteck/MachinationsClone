using System;

namespace MachinationsClone.Models.DTOs.Response
{
    public class GraphStatesGroupDto
    {
        public Guid Id { get; set; }
        public Guid GraphId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalSteps { get; set; }
        
        public static GraphStatesGroupDto FromGraphStatesGroup(Entities.Graph.GraphStatesGroup graphStatesGroup)
        {
            if (graphStatesGroup == null)
            {
                return null;
            }
            
            return new GraphStatesGroupDto
            {
                Id = graphStatesGroup.Id,
                GraphId = graphStatesGroup.GraphId,
                Order = graphStatesGroup.Order,
                CreatedAt = graphStatesGroup.CreatedAt,
                TotalSteps = graphStatesGroup.TotalSteps
            };
        }
    }
}