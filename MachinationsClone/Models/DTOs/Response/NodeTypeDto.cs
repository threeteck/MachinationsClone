using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.DTOs.Response
{
    public class NodeTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public string Symbol { get; set; } = "";
        public bool Exportable { get; set; } = false;
        
        public static NodeTypeDto FromNodeType(NodeType nodeType)
        {
            return new NodeTypeDto
            {
                Name = nodeType.Name,
                Description = nodeType.Description,
                Symbol = nodeType.Symbol,
                Exportable = nodeType.Exportable
            };
        }
    }
}