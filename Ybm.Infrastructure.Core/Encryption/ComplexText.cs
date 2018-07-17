
using System;
using System.Collections.Generic;
using System.Text;
using Ybm.Infrastructure.Core.PersianCaptchaHandler;

namespace Ybm.Infrastructure.Core.Encryption
{
    public static class ComplexText
    {

        public static string Generatekey()
        {
            var st0 = DateTime.Now.Ticks.ToString().GetHashCode().ToString("x");
            var st1 = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            var st2 = DateTime.Now.AddMinutes(-1).Ticks.ToString().GetHashCode().ToString("x");

            var apiKey = st0 + st1 + st2;
            return apiKey;
        }

        public static string GenerateLongerkey()
        {
            var st0 = DateTime.Now.Ticks.ToString().GetHashCode().ToString("x");
            var st1 = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            var st2 = DateTime.Now.AddMinutes(-1).Ticks.ToString().GetHashCode().ToString("x");

            var apiKey = st2 + st1 + st0 + Generatekey();
            return apiKey;
        }

        public static string GetEncrypted(this string plainText)
        {
            var encryptedText = Encryptor.Encrypt(plainText);
            var base64String = Base64Helper.StringToBase64(encryptedText);
            return base64String;
        }

        public static string GetDecrypted(this string bas64String)
        {
            var encrypted = Base64Helper.Base64ToString(bas64String);
            var decryptedText = Encryptor.Decrypt(encrypted);
            return decryptedText;
        }

        public static string CreatePassword(int length)
        {
            const string valid = "a!b@c#d$e%f^g&h*i(j)k)l(m*n7o^p%q$r#s@t!u!v@w#x$y%z^A^B&C*(D)E$F%G@H!I%J&K@L~M1N2@O#3$P%3^Q&4*R(5)S6T7@U#8$V535W63X2Y1Z1112345678!90";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}
