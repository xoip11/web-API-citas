using System.ComponentModel.DataAnnotations;

namespace API_Citas_Uts.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }
}
