using System.Security.Cryptography;
using System.Text;

namespace Trigger.BCL.Common.Security
{
    public static class SecureText
    {
        public static string Secure(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] encoded = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(encoded); 

            return System.BitConverter.ToString(result); 
        }

        public static bool Compare(string input, string hash)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] encoded = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(encoded); 

            return System.BitConverter.ToString(result).Equals(hash);
        }
    }
}
