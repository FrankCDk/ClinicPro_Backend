namespace ClinicPro.Application.Dtos.Login
{
    public class LoginResponse
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
