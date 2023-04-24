using System;

namespace MachinationsClone.Models.DTOs.Request
{
    public class AddConnectionDto
    {
        public string ConnectionTypeName { get; set; }
        
        public Guid? StartId { get; set; }
        public Guid? EndId { get; set; }
    }
}