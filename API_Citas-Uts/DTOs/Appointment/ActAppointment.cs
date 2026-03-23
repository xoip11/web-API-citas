namespace API_Citas_Uts.DTOs.Appointment
{
    public class ActAppointment
    {
        public int IdCita { get; set; }

        public int IdUsuarioEstudiante { get; set; }

        public int IdEspecialista { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string? Descripcion { get; set; }
    }
}
