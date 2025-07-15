using System.Security.Cryptography;
using System.Text;

namespace ClothingWebsite.Server.DTOs
{
    public class HashPassword
    {        
        public string HashSHA256(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return "";
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(inputBytes);
            StringBuilder sb = new();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }        
    }
}