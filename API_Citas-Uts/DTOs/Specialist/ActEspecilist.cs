using System.ComponentModel.DataAnnotations;

namespace API_Citas_Uts.DTOs.Specialist
{
    public class ActEspecilist
    {
        [Required]
        public int IdEspecialista { get; set; }

        public string Especialidad { get; set; }
        public string Consultorio { get; set; }
        public string Horario { get; set; }
        public bool? Activo { get; set; }

    }
}
