using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Pruebas
{
    internal class ValidadorSeguridad
    {
        /// <summary>
        /// valida si una contraseña cumple con los requisitos minimos de seguridad.
        /// Requisito de Caja Negra: Mínimo 8 caracteres.
        /// </summary>
        /// <param name="password">La cadena a validar.</param>
        /// <returns>True si es válida, False en caso contrario.</returns>
      
        public bool EsValida(string password)
        {
            // Verificamos primero que no sea nula para evitar excepciones
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            // Aplicamos la regla de negocio: Longitud mínima de 8
            if (password.Length >= 8)
            {
                return true;
            }
            return false;
        }
    }
}
