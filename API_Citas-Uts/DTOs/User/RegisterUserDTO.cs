using System.ComponentModel.DataAnnotations;

namespace API_Citas_Uts.DTOs.User
{
    public class RegisterUserDTO
    {
        [Required]
        public string nombreUsuario { get; set; }

        [Required]
        public string Rol { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }

        public string Carrera { get; set; }
        public string telefono { get; set; }
    }
}
