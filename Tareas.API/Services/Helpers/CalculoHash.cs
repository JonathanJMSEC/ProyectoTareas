using System.Security.Cryptography;

namespace Services.Helpers
{
    public static class CalculoHash
    {
        public static string GenerarHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerificarHash(string input, string hash)
        {
            var hashGenerado = GenerarHash(input);
            return hashGenerado == hash;
        }
    }
}