namespace ASP_NET_API
{
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    public class Hash
    {
        static readonly byte[] salt = Encoding.ASCII.GetBytes("vcE7m0m2CHjB2Y4gurbEEA==");
        public static string Hashing(string password)
        {
            /*        byte[] salt = new byte[128 / 8];
                    using (var rngCsp = new RNGCryptoServiceProvider())
                    {
                        rngCsp.GetNonZeroBytes(salt);
                    }*/

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        public static bool Verify(string password, string hashedPassword)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed == hashedPassword;

        }
    }
}
