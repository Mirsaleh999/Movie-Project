using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Movies.WebAdmin.ViewModels
{
    public class LoginViewModel
    {

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə lazımdır!")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Parolunuz ən azı 4 simvoldan ibarət olmalıdır!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
