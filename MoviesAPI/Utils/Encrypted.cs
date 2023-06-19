using XSystem.Security.Cryptography;

namespace MoviesAPI.Utils
{
    public static class Encrypted
    {
        /// <summary>
        /// Method to encrypt the password with MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetMd5(string password)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            string response = string.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                response += data[i].ToString("x2").ToLower();
            }

            return response;
        }
    }
}
