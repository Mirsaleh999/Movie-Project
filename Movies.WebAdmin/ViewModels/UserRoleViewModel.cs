namespace Movies.WebAdmin.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserFullName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
