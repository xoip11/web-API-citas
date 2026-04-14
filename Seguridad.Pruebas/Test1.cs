using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Seguridad.Pruebas
{
    [TestClass]
    public class ValidadorTests
    {
        [TestMethod]
        public void ValidarLongitud_ContrasenaCorta_RetornaFalse()
        {
            // Arrange (Preparar) - Datos de entrada
            string passwordInvalido = "abc";
            var validador = new ValidadorSeguridad(); // Clase que vamos a probar
                                                      // Act (Actuar) - Ejecutar la funcionalidad
            bool resultado = validador.EsValida(passwordInvalido);
            // Assert (Afirmar) - Verificar si el resultado es el esperado
            Assert.IsFalse(resultado, "Una contraseña de 3 caracteres no deberíaser válida.");
        }
        [TestMethod]
        public void ValidarLongitud_ContrasenaOk_RetornaTrue()
        {
            // Arrange (Preparar) - Datos de entrada
            string passwordValido = "abc12345";
            var validador = new ValidadorSeguridad(); // Clase que vamos a probar

            // Act (Actuar) - Ejecutar la funcionalidad
            bool resultado = validador.EsValida(passwordValido);
            // Assert (Afirmar) - Verificar si el resultado es el esperado
            Assert.IsTrue(resultado);
        }
    }
}