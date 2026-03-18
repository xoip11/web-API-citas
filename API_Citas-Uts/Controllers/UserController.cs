using API_Citas_Uts.DTOs.User;
using API_Citas_Uts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace API_Citas_Uts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("AggUsuario")]
        public IActionResult InsertarUsuario([FromBody] InsUserDTO usuario)
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

        [HttpPut("Act/Users")]
        public IActionResult ActualizarUsuarios(User Usuario)
        {
            if (Usuario == null || Usuario.IdUsuario <= 0)
                return BadRequest("Datos de el usuario no válidos.");
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand cmd = new SqlCommand("Act_Users", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Usuario.IdUsuario;
                    cmd.Parameters.Add("@Nombre_Usuario", SqlDbType.VarChar, 50).Value = Usuario.nombreUsuario;
                    cmd.Parameters.Add("@Rol", SqlDbType.VarChar, 20).Value = Usuario.Rol;
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 50).Value = Usuario.Correo;
                    cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = Usuario.activo;
                    cmd.Parameters.Add("@Carrera", SqlDbType.VarChar, 100).Value = Usuario.Carrera;
                    cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = Usuario.telefono;
                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    int rowsAffected = result != null ? Convert.ToInt32(result) : 0;

                    if (rowsAffected > 0)
                        return Ok("Datos de Usuario actualizados correctamente.");
                    else
                        return NotFound("Usuario no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");

            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        [HttpPut("act/contra")]
        public IActionResult ActualizarContra(User Usuario)
        {
            if (Usuario == null || Usuario.IdUsuario <= 0 || string.IsNullOrEmpty(Usuario.Contrasena))
                return BadRequest("Datos de el usuario no válidos.");

            try
            {
                string hashedPassword = HashPassword(Usuario.Contrasena);

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand cmd = new SqlCommand("Act_UserContrasena", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Usuario.IdUsuario;
                    cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar, 100).Value = hashedPassword;

                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    int rowsAffected = result != null ? Convert.ToInt32(result) : 0;

                    if (rowsAffected > 0)
                        return Ok("Contraseña actualizada correctamente.");
                    else
                        return NotFound("Usuario no encontrado o contraseña incorrecta.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }
        [HttpGet("buscar/User/{id}")]
        public IActionResult BuscarUser(int id)
        {
            if (id <= 0)
                return BadRequest("ID de Usuario no válido.");

            User Usuario = null;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Bus_Users", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = id;

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Usuario = new User
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            nombreUsuario = reader["NombreUsuario"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            activo = Convert.ToBoolean(reader["Activo"]),
                            Carrera = reader["Carrera"].ToString(),
                            telefono = reader["Telefono"].ToString()
                        };
                    }
                }
            }

            if (Usuario == null)
                return NotFound("Usuario no encontrado.");

            return Ok(Usuario);
        }
    }
}
