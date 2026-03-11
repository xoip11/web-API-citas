namespace API_Citas_Uts.Models
{
    public class Specialist
    {
        public int IdEspecialista { get; set; }
        public int IdUsuario { get; set; }
        public string Especialidad { get; set; }
        public string Consultorio { get; set; }
        public string horario { get; set; }
        public bool activo { get; set; }
    }
}
