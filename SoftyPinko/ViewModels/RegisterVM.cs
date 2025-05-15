using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace SoftyPinko.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(30)]
        public  string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)][Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
