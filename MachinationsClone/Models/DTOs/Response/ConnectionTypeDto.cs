using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.DTOs.Response
{
    public class ConnectionTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public LineType LineType { get; set; } = LineType.Solid;
        
        public static ConnectionTypeDto FromConnectionType(ConnectionType connectionType)
        {
            return new ConnectionTypeDto
            {
                Name = connectionType.Name,
                Description = connectionType.Description,
                LineType = connectionType.LineType
            };
        }
    }
}