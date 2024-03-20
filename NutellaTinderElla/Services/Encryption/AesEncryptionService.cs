using System;
using System.Security.Cryptography;
using System.Text;

namespace NutellaTinderElla.Services.Encryption
{
    public class AesEncryptionService
    {
        private readonly byte[] key;

        public AesEncryptionService(IConfiguration configuration)
        {
            string? secretKey = configuration?["Encryption:SecretKey"];

            if (secretKey == null)
            {
                throw new ArgumentNullException(nameof(secretKey), "Encryption secret key is not provided.");
            }

            // Generate a 256-bit key
            using (SHA256 sha = SHA256.Create())
            {
                key = sha.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
            }
        }

        public string Encrypt(string plaintext, out byte[] iv)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.GenerateIV();
            iv = aesAlg.IV;

            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.Write(plaintext);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string ciphertext, byte[] iv)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(ciphertext);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = key;
                aesAlg.IV = iv; // Use the provided IV for decryption
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                // Convert the decrypted bytes to a string
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                return decryptedText;
            }
            catch (FormatException ex)
            {
                // Handle base64 decoding errors
                throw new CryptographicException("Invalid base64 string format.", ex);
            }
            catch (CryptographicException ex)
            {
                // Handle cryptographic errors
                throw new CryptographicException("Error occurred during decryption.", ex);
            }
            catch (Exception ex)
            {
                // Handle other unexpected errors
                throw new Exception("An unexpected error occurred during decryption.", ex);
            }
        }
    }
}
