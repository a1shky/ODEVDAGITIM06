using System.ComponentModel.DataAnnotations;

namespace ODEVDAGITIM06.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}