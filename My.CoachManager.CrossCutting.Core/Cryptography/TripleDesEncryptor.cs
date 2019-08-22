using System;
using System.Security.Cryptography;
using System.Text;

namespace My.CoachManager.CrossCutting.Core.Cryptography
{
    /// <summary>
    /// Helper For Encrypt/Decrypt.
    /// </summary>
    public static class TripleDesEncryptor
    {
        #region ----- Methods -----

        /// <summary>
        /// Crypt a password in triple DES format.
        /// </summary>
        /// <param name="original">The password to hash.</param>
        /// <param name="key">The crypt key.</param>
        /// <returns>Encrypted string.</returns>
        public static string Encrypt(string original, string key)
        {
            using (var hashMd5 = new MD5CryptoServiceProvider())
            {
                var passwordHash = hashMd5.ComputeHash(Encoding.Default.GetBytes(key));
                using (var des = new TripleDESCryptoServiceProvider())
                {
                    des.Key = passwordHash;
                    des.Mode = CipherMode.ECB;
                    var buffer = Encoding.Default.GetBytes(original);
                    return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
                }
            }
        }

        /// <summary>
        /// Decrypt a triple DES password.
        /// </summary>
        /// <param name="encrypted">The encrypted password.</param>
        /// <param name="key">The crypt key.</param>
        /// <returns>Decrypted string.</returns>
        public static string Decrypt(string encrypted, string key)
        {
            encrypted = Encoding.Default.GetString(Convert.FromBase64String(encrypted));
            using (var hashMd5 = new MD5CryptoServiceProvider())
            {
                var passwordHash = hashMd5.ComputeHash(Encoding.Default.GetBytes(key));
                using (var des = new TripleDESCryptoServiceProvider())
                {
                    des.Key = passwordHash;
                    des.Mode = CipherMode.ECB;
                    var buffer = Encoding.Default.GetBytes(encrypted);
                    return Encoding.Default.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
                }
            }
        }

        #endregion ----- Methods -----
    }
}
