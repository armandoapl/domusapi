using System.ComponentModel.DataAnnotations;

namespace Tesis.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
