using System.ComponentModel.DataAnnotations;

namespace FurnitureOnlineStore.Models.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mailul este obligatoriu")]
        [Display(Name = "E-mail")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este necesara")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Display(Name = "Tine minte??")] 
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}