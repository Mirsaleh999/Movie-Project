using System.ComponentModel.DataAnnotations;

namespace Movie.WEBUI.wwwroot.ViewModels
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
