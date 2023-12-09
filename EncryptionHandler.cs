using System;
using System.IO;
using System.Security.Cryptography;

public class EncryptionHandler
{
    private Aes encrypter;

	public EncryptionHandler()
	{
        encrypter = Aes.Create();
	}

    public byte[] convertToBytes(String toEncrypt)
    {
        return EncryptStringToBytes_Aes(toEncrypt, encrypter.Key, encrypter.IV);
    }

    public String convertToString(byte[] toDecrypt) {
        return DecryptStringFromBytes_Aes(toDecrypt, encrypter.Key, encrypter.IV);
    }

    //https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;
        // Create a new AesManaged.
        
        // Create encryptor
        ICryptoTransform _encryptor = encrypter.CreateEncryptor(Key, IV);
        // Create MemoryStream
        using (MemoryStream ms = new MemoryStream())
        {
            // Create crypto stream using the CryptoStream class. This class is the key to encryption
            // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
            // to encrypt
            using (CryptoStream cs = new CryptoStream(ms, _encryptor, CryptoStreamMode.Write))
            {
                // Create StreamWriter and write data to a stream
                using (StreamWriter sw = new StreamWriter(cs))
                    sw.Write(plainText);
                encrypted = ms.ToArray();
                cs.Flush();
            }
          }
        // Return encrypted data
        return encrypted;
    }


    string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        string? plaintext = null;
        // Create AesManaged
        using (Aes aes = Aes.Create())
        {
            // Create a decryptor
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            // Create the streams used for decryption.
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                // Create crypto stream
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    // Read crypto stream
                    using (StreamReader reader = new StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }
}
