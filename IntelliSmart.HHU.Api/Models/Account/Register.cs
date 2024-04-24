using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IntelliSmart.HHU.Api.Models.Account
{
    public class Register
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

        [DefaultValue("")]
        public string Discomm { get; set; }

        [Range(1, 2, ErrorMessage = "Role must be either 1 (Admin) or 2 (User)")]
        public UserRole Role { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }
    }
}