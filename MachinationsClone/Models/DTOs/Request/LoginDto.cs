using System.ComponentModel.DataAnnotations;

namespace MachinationsClone.Models.DTOs.Request
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public bool RememberMe { get; set; }
    }
}