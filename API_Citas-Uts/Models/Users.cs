namespace API_Citas_Uts.Models
{
    public class User
    {
        public int IdUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string Rol { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool activo { get; set; }
        public string Carrera { get; set; }
        public string telefono { get; set; }
    }
}
