using System.ComponentModel.DataAnnotations;

namespace SoftyPinko.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MaxLength(50)]
        public string UserNameOrEmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
