namespace Showcase.Services.Common.Extensions
{
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptionExtensions
    {
        private const string Pepper = "T3L3R1K 4C4D3MY";

        public static string ToMd5Hash(this int input)
        {
            return input.ToString().ToMd5Hash();
        }

        public static string ToMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}{1}", input, Pepper)));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (byte symbol in data)
            {
                builder.Append(symbol.ToString("x2"));
            }

            // Return the hexadecimal string.
            return builder.ToString();
        }
    }
}
