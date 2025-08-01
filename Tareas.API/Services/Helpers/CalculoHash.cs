using System.Security.Cryptography;

namespace Services.Helpers
{
    /// <summary>
    /// Clase para calcular hashes de contraseñas.
    /// /// </summary>
    public static class CalculoHash
    {
        /// <summary>
        /// Genera un hash SHA256 a partir de una cadena de texto.
        /// /// </summary>
        /// <param name="input">Cadena de texto a hashear.</param>
        public static string GenerarHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifica si un hash coincide con el hash generado a partir de una cadena de texto
        /// /// </summary>
        /// <param name="input">Cadena de texto a verificar.</param>
        /// <param name="hash">Hash a comparar.</param>
        /// <returns>True si el hash coincide, false en caso contrario.</returns>
        public static bool VerificarHash(string input, string hash)
        {
            var hashGenerado = GenerarHash(input);
            return hashGenerado == hash;
        }
    }
}