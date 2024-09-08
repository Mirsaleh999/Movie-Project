using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Movies.WebAdmin.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Img { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
    }
}
