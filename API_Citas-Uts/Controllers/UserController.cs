using API_Citas_Uts.DTOs.User;
using API_Citas_Uts.Models;
using Microsoft.AspNetCore.Identity;
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
        // El constructor del controlador recibe una instancia de IConfiguration
        // para acceder a la cadena de conexión a la base de datos.
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // El método InsertarUsuario recibe un objeto InsUserDTO con los datos del nuevo usuario, y los manda a la bd
        [HttpPost("AggUsuario")]
        public IActionResult InsertarUsuario([FromBody] InsUserDTO usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.nombreUsuario) || string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                return BadRequest("Datos de usuario no válidos.");
            }

            string hashedPassword = _hasher.HashPassword(null, usuario.Contrasena);

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand cmd = new SqlCommand("Ins_Users", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre_usuario", SqlDbType.VarChar, 100).Value = usuario.nombreUsuario;
                    cmd.Parameters.Add("@Rol", SqlDbType.VarChar, 50).Value = usuario.Rol;
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = usuario.Correo;
                    cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar, 200).Value = hashedPassword;
                    cmd.Parameters.Add("@Carrera", SqlDbType.VarChar, 100).Value = usuario.Carrera;
                    cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = usuario.telefono;

                    conn.Open();
                    int idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return Ok("Usuario creado correctamente");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Rol no válido"))
                    return BadRequest("El rol debe ser: Administrador, Especialista o Estudiante");

                return StatusCode(500, "Error interno");
            }
        }
        // El método ActualizarUsuarios recibe un objeto ActuUserDTO con los datos actualizados del usuario, y los manda a la bd
        [HttpPut("Act/Users")]
        public IActionResult ActualizarUsuarios(ActuUserDTO Usuario)
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
        // Se crea una instancia de PasswordHasher para hashear las contraseñas antes de guardarlas en la base de datos.
        private readonly PasswordHasher<string> _hasher = new PasswordHasher<string>();

        // El método ActualizarContra recibe un objeto ActuPasswordDTO con la nueva contraseña del usuario,
        // la hashea y la manda a la bd
        [HttpPut("act/contra")]
        public IActionResult ActualizarContra(ActuPasswordDTO Usuario)
        {
            if (Usuario == null || Usuario.IdUsuario <= 0 || string.IsNullOrEmpty(Usuario.Contrasena))
                return BadRequest("Datos de el usuario no válidos.");

            try
            {
                string hashedPassword = _hasher.HashPassword(null, Usuario.Contrasena);

                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand cmd = new SqlCommand("Act_UserContrasena", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Usuario.IdUsuario;
                    cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 200).Value = hashedPassword;

                    conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        return Ok("Contraseña actualizada correctamente.");
                    else
                        return NotFound("Usuario no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }
        // El método BuscarUser recibe un ID de usuario por la URL, lo busca en la base de datos y devuelve sus datos.
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
        // El método EliminarUsuario recibe un ID de usuario por la URL, lo busca en la base de datos y lo pone en estado inactivo,
        // para no perder la integridad referencial de la base de datos,
        // ya que el usuario puede estar relacionado con otras tablas como especialista o citas.
        [HttpDelete("BorrarUser/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            if (id <= 0)
                return BadRequest("ID de Usuario no valido.");
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Bor_Users", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", id);
                conn.Open();
                int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                if (rowsAffected > 0)
                    return Ok("Usuario eliminado correctamente.");
                else
                    return NotFound("No existe este usuario.");
            }
        }
        // El método ListarUsuarios recibe dos parámetros opcionales por la URL, nombre y rol,
        // y devuelve una lista de usuarios que coincidan con esos parámetros
        [HttpGet("ListarUsers")]
        public IActionResult ListarUsuarios(string? nombre = null, string? rol = null)
        {
            List<User> usuarios = new List<User>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("List_Users", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", (object?)nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rol", (object?)rol ?? DBNull.Value);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User usuario = new User
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            nombreUsuario = reader["NombreUsuario"].ToString(),
                            Rol = reader["Rol"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            activo = Convert.ToBoolean(reader["Activo"]),
                            Carrera = reader["Carrera"].ToString(),
                            telefono = reader["Telefono"].ToString()
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
            if (usuarios.Count == 0)
            {
                return Ok(new
                {
                    mensaje = "No se encontraron usuarios",
                    data = usuarios
                });
            }

            return Ok(usuarios);
        }

    }
    
}
