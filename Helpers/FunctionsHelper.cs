using System.Security.Cryptography;
using System.Text;

namespace GestionAcademicaAPI.Helpers
{
    /// <summary>
    /// Provides helper methods for security-related functions.
    /// </summary>
    public static class FunctionsHelper
    {
        /// <summary>
        /// Hashes the specified password using SHA-256.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password as a base64-encoded string.</returns>
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Verifies that the specified password matches the hashed password.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="hashedPassword">The hashed password to compare against.</param>
        /// <returns><c>true</c> if the password matches the hashed password; otherwise, <c>false</c>.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}
