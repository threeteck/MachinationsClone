using System.ComponentModel.DataAnnotations;

namespace MachinationsClone.Models.DTOs.Request
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }
    }
}