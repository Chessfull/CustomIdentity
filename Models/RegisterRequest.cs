using System.ComponentModel.DataAnnotations;

namespace CustomIdentity.Models
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [Length(8,30)]
        public string Password { get; set; }
    }
}
