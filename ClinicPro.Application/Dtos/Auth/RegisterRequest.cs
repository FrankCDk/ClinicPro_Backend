namespace ClinicPro.Application.Dtos.Auth
{
    public class RegisterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public int IsActive { get; set; } = 1;
    }
}
