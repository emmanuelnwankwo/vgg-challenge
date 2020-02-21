using System.ComponentModel.DataAnnotations;

namespace WebApp.Core.Dtos
{
    public class UserRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
