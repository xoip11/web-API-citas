using API_Citas_Uts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_Citas_Uts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppointmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult ListarAppointment()
        {
          List<Appointment> citas = new List<Appointment>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("List_Appointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Appointment cita = new Appointment();
                    
                        cita.IdCita = Convert.ToInt32(reader["idCita"]);
                        cita.IdUsuarioEstudiante = Convert.ToInt32(reader["IdUsuarioEstudiante"]);
                        cita.IdEspecialista = Convert.ToInt32(reader["IdEspecialista"]);
                        cita.Fecha = Convert.ToDateTime(reader["fecha"]);
                        cita.Hora = (TimeSpan)reader["hora"];
                        cita.Descripcion = reader["descripcion"].ToString();
                        cita.Estado = reader["estado"].ToString();

                        citas.Add(cita);
                }
            }
            return Ok(citas);
        }
    }
}
