using System.ComponentModel.DataAnnotations;

namespace MachinationsClone.Models.DTOs.Request
{
    public class UpdateGraphDto
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
        
        public int StepSize { get; set; }
    }
}