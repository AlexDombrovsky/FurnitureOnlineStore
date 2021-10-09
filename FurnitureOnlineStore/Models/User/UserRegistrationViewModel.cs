using System.ComponentModel.DataAnnotations;

namespace FurnitureOnlineStore.Models.User
{
    public class UserRegistrationViewModel
    {
        [EmailAddress(ErrorMessage = "Adresa email invalida")]
        [Required(ErrorMessage = "E-mailul este obligatoriu")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este necesara")]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmați parola este necesară")]
        [Compare("Password", ErrorMessage = "Parola și Confirmarea parolei nu se potrivesc.")]
        [Display(Name = "Confirmă parola")]
        public string PasswordConfirm { get; set; }
    }
}