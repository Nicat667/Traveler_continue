using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPAssword { get; set; }
        [Required]
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}
