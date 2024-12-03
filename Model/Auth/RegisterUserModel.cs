using System.ComponentModel.DataAnnotations;

namespace Model.Auth
{
    public class RegisterUserModelPost
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}