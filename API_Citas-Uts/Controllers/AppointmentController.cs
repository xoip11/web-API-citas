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
        [HttpPost]
        public IActionResult AgregarAppointment([FromBody] Appointment cita)
        {
            if (cita == null)
                return BadRequest("Datos de cita no proporcionados.");

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Ins_Appointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuarioEstudiante", cita.IdUsuarioEstudiante);
                cmd.Parameters.AddWithValue("@IdEspecialista", cita.IdEspecialista);
                cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
                cmd.Parameters.AddWithValue("@hora", cita.Hora);
                cmd.Parameters.AddWithValue("@descripcion", cita.Descripcion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Ok("Cita creada correctamente");
        }

        [HttpPut]
        public IActionResult ActualizarAppointment(Appointment cita)
        {
            if (cita == null || cita.IdCita <= 0)
                return BadRequest("Datos de cita no válidos.");

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Act_Appointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCita", cita.IdCita);
                cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
                cmd.Parameters.AddWithValue("@hora", cita.Hora);
                cmd.Parameters.AddWithValue("@descripcion", cita.Descripcion);
                conn.Open();

                object result = cmd.ExecuteScalar();
                int rowsAffected = result != null ? Convert.ToInt32(result) : 0;

                if (rowsAffected > 0)
                    return Ok("cita actualizada correctamente.");
                else
                    return NotFound("Cita no encontrada.");
            }
        }
    }
}
