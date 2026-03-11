namespace API_Citas_Uts.Models
{
    public class Appointment
    {
        public int IdCita { get; set; }

        public int IdUsuarioEstudiante { get; set; }

        public int IdEspecialista { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }
    }
}
