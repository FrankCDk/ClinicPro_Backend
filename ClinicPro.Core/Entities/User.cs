namespace ClinicPro.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public int UserRol { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserPasswordHash { get; set; } = string.Empty;
        public DateOnly UserDateBirth { get; set; }
        public bool UserIsActive { get; set; } = false;

    }
}
