using System.Security.Cryptography;
using System.Text;

namespace NutellaTinderElla.Services.Encryption
{
    public class AesEncryptionService
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public AesEncryptionService(IConfiguration configuration)
        {

            string secretKey = configuration["Encryption:SecretKey"];

            // Generate a 256-bit key
            using (SHA256 sha = SHA256.Create())
            {
                key = sha.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
            }

            // Generate a 128-bit IV
            iv = new byte[16];
            new Random().NextBytes(iv);
        }

        public string Encrypt(string plaintext)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.Write(plaintext);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string ciphertext)
        {
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(cipherBytes);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
