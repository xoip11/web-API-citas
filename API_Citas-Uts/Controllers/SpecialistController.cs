using API_Citas_Uts.DTOs.Appointment;
using API_Citas_Uts.DTOs.Specialist;
using API_Citas_Uts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
namespace API_Citas_Uts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SpecialistController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("agg/Speci")]
        public IActionResult AgregarEspecialista([FromBody] InsSpecialist especialista)
        {
            if (especialista == null)
                return BadRequest("Datos de especialista no proporcionados.");
            try
            {

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand cmd = new SqlCommand("Ins_Specialist", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", especialista.IdUsuario);
                    cmd.Parameters.AddWithValue("@Especialidad", especialista.Especialidad);
                    cmd.Parameters.AddWithValue("@Consultorio", especialista.Consultorio);
                    cmd.Parameters.AddWithValue("@Horario", especialista.horario);
                    conn.Open();
                    int idEspecialista = Convert.ToInt32(cmd.ExecuteScalar()); 
                }
                return Ok("Especialista creado correctamente");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return BadRequest("Este usuario ya está registrado como especialista.");
                }

                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Listar/Speci")]
        public IActionResult ListarSpecialist(string? especialidad = null)
        {

            List<ListSpecialist> especialistas = new List<ListSpecialist>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("List_Specialist", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Especialidad", (object?)especialidad ?? DBNull.Value);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListSpecialist especialista = new ListSpecialist();
                    especialista.IdEspecialista = Convert.ToInt32(reader["IdEspecialista"]);
                    especialista.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                    especialista.Especialidad = reader["Especialidad"].ToString();
                    especialista.Consultorio = reader["Consultorio"].ToString();
                    especialista.horario = reader["Horario"].ToString();
                    especialistas.Add(especialista);
                }
            }
            return Ok(especialistas);
        }
        [HttpGet("{id}")]
        public IActionResult BuscarSpecialist(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Bus_Specialist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEspecialista", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var esp = new Specialist
                    {
                        IdEspecialista = Convert.ToInt32(reader["IdEspecialista"]),
                        Especialidad = reader["Especialidad"].ToString(),
                        Consultorio = reader["Consultorio"].ToString(),
                        horario = reader["Horario"].ToString()
                    };

                    return Ok(esp);
                }
            }

            return NotFound();
        }
        [HttpPut]
        public IActionResult ActualizarSpecialist(ActEspecilist esp)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Act_Specialist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEspecialista", esp.IdEspecialista);
                cmd.Parameters.AddWithValue("@Especialidad", (object?)esp.Especialidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Consultorio", (object?)esp.Consultorio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Horario", (object?)esp.Horario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Activo", esp.Activo);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Actualizado");
        }
        [HttpDelete("{id}")]
        public IActionResult EliminarSpecialist(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Bor_Specialist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEspecialista", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Eliminado");
        }
    }
}
