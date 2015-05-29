using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace beneDesCYC.core
{
    public class pwdHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        public static string Encrypt(string password)
        {
            byte[] by = new byte[password.Length];
            by = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(by);
        }
        /// <summary>
        /// 解密
        /// </summary>
        public static string Decrypt(string password)
        {
            byte[] by = Convert.FromBase64String(password);
            return Encoding.UTF8.GetString(by);
        }

       
    }
}
