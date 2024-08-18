using System.ComponentModel.DataAnnotations;

namespace XuongMay.Models
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}