using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Wpf_Online_Shop.Model
{
    public static class Hasher
    {
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] bytesEncrypted = sha1.ComputeHash(bytes);
            return Convert.ToBase64String(bytesEncrypted);

        }
    }
}
