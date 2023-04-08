using System.Security.Cryptography;
using System.Text;

namespace ReceitasDeFamilia.Services
{
    public static class SaltService
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        //public static string HashPasword(string password, out byte[] salt)
        //{
        //    salt = RandomNumberGenerator.GetBytes(keySize);

        //    var hash = Rfc2898DeriveBytes.Pbkdf2(
        //        Encoding.UTF8.GetBytes(password),
        //        salt,
        //        iterations,
        //        hashAlgorithm,
        //        keySize);

        //    return Convert.ToBase64String(hash);
        //}
        public static string HashPasword(string password, out string salt)
        {
            var saltHash = RandomNumberGenerator.GetBytes(keySize);
            salt = Convert.ToBase64String(saltHash);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltHash,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToBase64String(hash);
        }
        private static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromBase64String(hash));
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var saltHash = Convert.FromBase64String(salt);
            return VerifyPassword(password, hash, saltHash);
        }
    }
}
