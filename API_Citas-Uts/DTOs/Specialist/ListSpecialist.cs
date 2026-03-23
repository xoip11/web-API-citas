namespace API_Citas_Uts.DTOs.Specialist
{
    public class ListSpecialist
    {
        public int IdEspecialista { get; set; }
        public int IdUsuario { get; set; }
        public string Especialidad { get; set; }
        public string Consultorio { get; set; }
        public string horario { get; set; }
        public bool activo { get; set; }
    }

}

