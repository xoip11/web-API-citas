using System.ComponentModel.DataAnnotations;

namespace API_Citas_Uts.DTOs.User
{
    public class ActuUserDTO
    {
        [Required]
        public int IdUsuario { get; set; }

        public string nombreUsuario { get; set; }
        public string Rol { get; set; }

        [EmailAddress]
        public string Correo { get; set; }
        public bool activo { get; set; }
        public string Carrera { get; set; }
        public string telefono { get; set; }
    }
}
