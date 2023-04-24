using System.ComponentModel.DataAnnotations;

namespace MachinationsClone.Models.Entities.Graph
{
    public enum LineType
    {
        Solid = 0,
        Dashed = 1,
    }
    
    public class ConnectionType
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public LineType LineType { get; set; } = LineType.Solid;

        public ConnectionType() {}
        
        public ConnectionType(string name, string description, LineType lineType)
        {
            Name = name;
            Description = description;
            LineType = lineType;
        }
    }
}