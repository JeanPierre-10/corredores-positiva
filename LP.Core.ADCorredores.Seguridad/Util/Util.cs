using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Seguridad.Util
{
    public class Util
    {
        public static String Hash(String textPlano)
        {
            byte[] salt;
            byte[] buffer;
            if (textPlano == null)
            {
                throw new ArgumentNullException(nameof(textPlano));
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(textPlano, 16, 1000))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(32);
            }
            byte[] dts = new byte[49];
            Buffer.BlockCopy(salt, 0, dts, 1, 16);
            Buffer.BlockCopy(buffer, 0, dts, 17, 32);

            return Convert.ToBase64String(dts);
        }

        public static bool VerifyHashedPassword(String hashedPassword, String password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 49) || (src[0] != 0))
            {
                return false;
            }
            byte[] dts = new byte[16];
            Buffer.BlockCopy(src, 1, dts, 0, 16);
            byte[] buffer3 = new byte[32];
            Buffer.BlockCopy(src, 17, buffer3, 0, 32);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dts, 1000))
            {
                buffer4 = bytes.GetBytes(32); ;
            }
            return buffer3.SequenceEqual(buffer4);
        }
    }
}
