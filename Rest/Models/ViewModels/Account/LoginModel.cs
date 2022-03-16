using System.ComponentModel.DataAnnotations;

namespace Rest.Models.ViewModels.Account
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Email daxil edilməyib.")]
        [EmailAddress(ErrorMessage = "Düzgün email daxil edin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edin.")]
        [MinLength(6,ErrorMessage = "Şifrə minimum 6 uzunluğunda olmalıdır.")]
        public string Password { get; set; }

    }
}
