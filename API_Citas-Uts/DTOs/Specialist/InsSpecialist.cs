using System.ComponentModel.DataAnnotations;

namespace API_Citas_Uts.DTOs.Specialist
{
    public class InsSpecialist
    {
       
        public int IdUsuario { get; set; }
        public string Especialidad { get; set; } 
        public string Consultorio { get; set; }
        public string horario { get; set; }  
        
    }
}