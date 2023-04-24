using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.DTOs.Request
{
    public class UpdateNodeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null;
        public ActivationModeEnum? ActivationMode { get; set; }
        public PullModeEnum? PullMode { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}