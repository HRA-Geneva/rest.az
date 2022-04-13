namespace Rest.Areas.Admin.Models.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserStatusId { get; set; }
        public string UserStatusName { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
    }
}
