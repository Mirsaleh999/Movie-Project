using System.ComponentModel;

namespace Movies.WebAdmin.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [DisplayName("Role adı")]
        public string Name { get; set; }
    }
}
