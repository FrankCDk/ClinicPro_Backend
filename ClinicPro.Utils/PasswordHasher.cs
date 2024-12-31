using System.Security.Cryptography;

namespace ClinicPro.Utils
{
    public static class PasswordHasher
    {
        // Método para hashear la contraseña
        public static string HashPassword(string password)
        {
            // Generar un "sal" (salt) aleatorio
            byte[] salt = GenerateSalt();

            // Hashear la contraseña con PBKDF2 usando SHA-256
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                // Obtener el hash resultante
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes de longitud para SHA-256

                // Combinar el "sal" y el hash en un solo array de bytes
                byte[] hashBytes = new byte[salt.Length + hash.Length];
                Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
                Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length);

                // Convertir el array de bytes combinado en una cadena Base64 para almacenamiento
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Método para verificar la contraseña
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Convertir el hash almacenado de Base64 a bytes
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // El "sal" está almacenado en los primeros 16 bytes del hash combinado
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);

            // El hash real está en los bytes restantes
            byte[] storedHashValue = new byte[hashBytes.Length - salt.Length];
            Buffer.BlockCopy(hashBytes, salt.Length, storedHashValue, 0, storedHashValue.Length);

            // Hashear la contraseña introducida con el mismo "sal"
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] computedHash = pbkdf2.GetBytes(32);

                // Comparar el hash calculado con el hash almacenado
                return AreHashesEqual(computedHash, storedHashValue);
            }
        }

        // Método para generar un "sal" aleatorio de 16 bytes
        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }

        // Método para comparar dos hashes de manera segura
        private static bool AreHashesEqual(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                return false;
            }

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
