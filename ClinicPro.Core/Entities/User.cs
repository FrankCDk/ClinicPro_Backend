namespace ClinicPro.Core.Entities
{
    public class User
    {
        public int Usr_id { get; set; }
        public string Usr_first_name { get; set; } = string.Empty;
        public string Usr_last_name { get; set; } = string.Empty;
        public string Usr_rol { get; set; } = string.Empty;
        public string Usr_email { get; set; } = string.Empty;
        public string Usr_password_hash { get; set; } = string.Empty;
        public DateOnly Usr_date_of_birth { get; set; }
        public bool Usr_is_active { get; set; } = false;

    }
}
