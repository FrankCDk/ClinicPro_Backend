namespace ClinicPro.Core.Entities
{
    public class Role
    { 
        public int RolId { get; set; }
        public string RolCode { get; set; } = string.Empty;
        public string RolName { get; set; } = string.Empty;
        public string RolDescription { get; set; } = string.Empty;
        public bool RolIsActive { get; set; } = true;
    }
}
