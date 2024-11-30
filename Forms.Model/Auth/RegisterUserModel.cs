using System.ComponentModel.DataAnnotations;

namespace Forms.Model.Auth
{
    public class RegisterUserModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
