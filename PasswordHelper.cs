
    using System.Security.Cryptography;
    using System.Text;

    namespace GlowMart.PasswordEnDe
    {
        public static class PasswordHelper
        {
            // 1. Fixed return type and added null check
            public static (string Hash, string Salt) HashPassword(string password)
            {
                if (string.IsNullOrEmpty(password))
                    throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

                using var hmac = new HMACSHA256();

                // hmac.Key automatically generates a unique cryptographically safe random salt
                string salt = Convert.ToBase64String(hmac.Key);
                string hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

                return (hash, salt);
            }

            // 2. Fixed verification checking
            public static bool VerifyPassword(string password, string storedHash, string storedSalt)
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
                    return false;

                byte[] saltBytes = Convert.FromBase64String(storedSalt);

                // Pass the original salt back into HMACSHA256 to recompute the exact same hash matrix
                using var hmac = new HMACSHA256(saltBytes);
                string computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

                return computedHash == storedHash;
            }
        }
    }
