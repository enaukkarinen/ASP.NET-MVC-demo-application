using System.ComponentModel.DataAnnotations;
using System;

namespace WJOArvioitava.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Käyttäjänimi")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nykyinen salasana")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Salasana {0} täytyy olla vähintään {2} merkkiä pitkä.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Uusi salasana")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Vahvista uusi salasana")]
        [Compare("NewPassword", ErrorMessage = "Uusi salasana ja vahvistus eivät ole samoja.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Käyttäjänimi")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Salasana")]
        public string Password { get; set; }

        [Display(Name = "Muista käyttäjänimi?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage="Käyttäjänimi on pakollinen")]
        [Display(Name = "Käyttäjänimi")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Salasana on pakollinen")]
        [StringLength(100, ErrorMessage = "Salasana {0} täytyy olla vähintään {2} merkkiä pitkä", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Salasana")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Vahvista salasana")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Salasana ja vahvistettu salasana eivät ole samoja.")]
        public string ConfirmPassword { get; set; }
    }
}
