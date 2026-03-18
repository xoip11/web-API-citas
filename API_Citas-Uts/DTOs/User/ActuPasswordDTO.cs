using System.ComponentModel.DataAnnotations;

namespace API_Citas_Uts.DTOs.User
{
    public class ActuPasswordDTO
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }
}
