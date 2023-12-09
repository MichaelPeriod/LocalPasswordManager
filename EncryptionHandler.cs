using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHandler
{
    public static byte[] encrypt(String plainText, String password)
    {
        byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
        Aes aes = Aes.Create();
        aes.Key = SHA256.Create().ComputeHash(passwordBytes);
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        ICryptoTransform enc = aes.CreateEncryptor(aes.Key, aes.IV);

        byte[] encrypted;

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, enc, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText.ToCharArray());
                }
                encrypted = aes.IV.Concat(msEncrypt.ToArray()).ToArray();
            }
        }

        return encrypted;
    }

    public static String decrypt(byte[] cypherText, String password)
    {
        byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
        Aes aes = Aes.Create();
        aes.Key = SHA256.Create().ComputeHash(passwordBytes);
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        String decrypted = "";

        byte[] iv = cypherText.Take(16).ToArray();
        //Need to remove IV from decrypted password consistently
        ICryptoTransform dec = aes.CreateDecryptor(aes.Key, iv);
        using (MemoryStream msDecrypt = new MemoryStream(cypherText.Skip(16).ToArray()))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, dec, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {

                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    decrypted = srDecrypt.ReadToEnd();
                }
            }
        }

        return decrypted;
    }
}
