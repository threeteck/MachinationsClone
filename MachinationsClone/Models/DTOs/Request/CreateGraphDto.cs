using System.ComponentModel.DataAnnotations;

namespace MachinationsClone.Models.DTOs.Request
{
    public class CreateGraphDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
    }
}