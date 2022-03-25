namespace OmnivusMvcWebsite.Models.ViewModels
{
    public class AdminCreateRoleViewModel
    {
        public AdminRolesViewModel AdminRoles { get; set; }
        //public CreateRoleModel NewRole { get; set; }
        public string NewRoleName { get; set; }
        public string OldRoleName { get; set; }
    }
}
