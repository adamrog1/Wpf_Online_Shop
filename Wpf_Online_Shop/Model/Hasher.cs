using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa zajmująca się przetwarzaniem haseł
    /// </summary>
    public static class Hasher
    {
        /// <summary>
        /// Metoda haszująca hasło za pomocą SHA1
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] bytesEncrypted = sha1.ComputeHash(bytes);
            return Convert.ToBase64String(bytesEncrypted);

        }
    }
}
