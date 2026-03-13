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

        [HttpGet("Listar")]
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
        [HttpPost("Crear")]
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

        [HttpPut("Actualizar")]
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

        [HttpDelete("Borrar/{id}")]
        public IActionResult CancelarAppointment(int id)
        {
            if (id <= 0)
                return BadRequest("ID de cita no válido.");
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Cancelar_Appointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCita", id);
                conn.Open();
                object result = cmd.ExecuteScalar();
                int rowsAffected = result != null ? Convert.ToInt32(result) : 0;
                if (rowsAffected > 0)
                    return Ok("Cita cancelada correctamente.");
                else
                    return NotFound("Cita no encontrada.");
            }
        }

        [HttpPut("confirmar/{id}")]
        public IActionResult ConfirmarAppointment(int id)
        {
            if (id <= 0)
                return BadRequest("ID de cita no válido.");
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Confirmar_Appointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCita", id);
                conn.Open();
                object result = cmd.ExecuteScalar();
                int rowsAffected = result != null ? Convert.ToInt32(result) : 0;
                if (rowsAffected > 0)
                    return Ok("Cita confirmada correctamente.");
                else
                    return NotFound("Cita no encontrada.");
            }
        }

        [HttpGet("buscar/{id}")]
        public IActionResult BuscarCita(int id)
        {
            if (id <= 0)
                return BadRequest("ID de cita no válido.");
            
            Appointment cita = null;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Bus_Appointment", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCita", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cita = new Appointment
                    {
                        IdCita = Convert.ToInt32(reader["idCita"]),
                        IdUsuarioEstudiante = Convert.ToInt32(reader["IdUsuarioEstudiante"]),
                        IdEspecialista = Convert.ToInt32(reader["IdEspecialista"]),
                        Fecha = Convert.ToDateTime(reader["fecha"]),
                        Hora = (TimeSpan)reader["hora"],
                        Descripcion = reader["descripcion"].ToString(),
                        Estado = reader["estado"].ToString()
                    };
                }
            }

            if (cita == null)
                return NotFound("Cita no encontrada.");

            return Ok(cita);
        }

        //METODOS RELACIONADOS DE ESTUDIANTES
        [HttpPost("AggUsuario")]
        public IActionResult InsertarUsuario([FromBody] User usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.nombreUsuario) || string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                return BadRequest("Datos de usuario no válidos.");
            }

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Ins_Users", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre_usuario", SqlDbType.VarChar, 100).Value = usuario.nombreUsuario;
                cmd.Parameters.Add("@Rol", SqlDbType.VarChar, 50).Value = usuario.Rol;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = usuario.Correo;
                cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar, 100).Value = usuario.Contrasena;
                cmd.Parameters.Add("@Carrera", SqlDbType.VarChar, 100).Value = usuario.Carrera;
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = usuario.telefono;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return Ok("Usuario creado correctamente");
        }
    }
}
