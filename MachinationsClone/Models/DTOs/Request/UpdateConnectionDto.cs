using System;
using System.Collections.Generic;

namespace MachinationsClone.Models.DTOs.Request
{
    public class UpdateConnectionDto
    {
        public Guid Id { get; set; }
        public string ConnectionTypeName { get; set; }
        public Guid? StartId { get; set; }
        public Guid? EndId { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}