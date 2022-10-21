using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace KarkaThamizha.Common.Utility
{
    public class Encryption
    {
        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string Encrypt(string inputValue)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                return inputValue;
            }

            ApplicationSettings appSettings = new ApplicationSettings();
            string EncryptionKey = appSettings.EncryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(inputValue);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    inputValue = Convert.ToBase64String(ms.ToArray());
                }
            }
            return inputValue;
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string Decrypt(string inputValue)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                return inputValue;
            }
            ApplicationSettings appSet = new ApplicationSettings();
            try
            {
                string EncryptionKey = appSet.EncryptionKey;
                inputValue = inputValue.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(inputValue);

                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        inputValue = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return inputValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Encode a string to a Url safe way.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string Encode(string inputValue)
        {
            return Encode(System.Text.Encoding.UTF8.GetBytes(inputValue));
        }

        /// <summary>
        /// Encode an array of byte to a Url safe way
        /// </summary>
        /// <param name="inputValues"></param>
        /// <returns></returns>
        public static string Encode(byte[] inputValues)
        {
            char[] resultValues = new char[inputValues.Length * 2];
            for (int i = 0; i < inputValues.Length; i++)
            {
                resultValues[i * 2] = (char)((inputValues[i] / 16) + 98);
                resultValues[(i * 2) + 1] = (char)((inputValues[i] & 15) + 98);
            }
            return new string(resultValues);
        }

        /// <summary>
        /// Decode a string encoded by Encode method
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string Decode(string inputValue)
        {
            if (inputValue == null)
            {
                return null;
            }
            return System.Text.Encoding.UTF8.GetString(DecodeToBytes(inputValue));
        }

        /// <summary>
        /// Decode a string to an array of bytes.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static byte[] DecodeToBytes(string inputValue)
        {
            byte[] inputValues = new byte[inputValue.Length / 2];
            for (int i = 0; i < inputValues.Length; i++)
            {
                byte bTop = (byte)(inputValue[i * 2] - 98);
                byte bBottom = (byte)(inputValue[(i * 2) + 1] - 98);

                inputValues[i] = (byte)(bTop * 16 + bBottom);
            }
            return inputValues;
        }
    }
}
