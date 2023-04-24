using System.ComponentModel.DataAnnotations;

namespace MachinationsClone.Models.Entities.Graph
{
    public class NodeType
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public string Symbol { get; set; } = "";
        public bool Exportable { get; set; } = false;
        
        public NodeType() {}
        
        public NodeType(string name, string description, string symbol, bool exportable = false)
        {
            Name = name;
            Description = description;
            Symbol = symbol;
            Exportable = exportable;
        }
    }
}