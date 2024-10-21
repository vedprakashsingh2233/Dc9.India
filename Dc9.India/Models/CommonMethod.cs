using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Dc9.India.Models
{
    public class CommonMethod
    {
        public static DataTable ExecuteProc(string Proc, String[,] arr)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ToString());
            SqlCommand com = new SqlCommand(Proc, conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            for (int i = 0; i < arr.Length / 2; i++)
            {
                com.Parameters.AddWithValue(arr[i, 0], arr[i, 1]);
            }
            SqlDataAdapter ad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public static string Encrypt(string Value, string EncryptionKey)
        {
            if (EncryptionKey.Trim() == "")
                EncryptionKey = "AMRESH";
            byte[] bytes = Encoding.Unicode.GetBytes(Value);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(EncryptionKey, new byte[13]
                {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
                });
                aes.Key = rfc2898DeriveBytes.GetBytes(32);
                aes.IV = rfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.Close();
                    }
                    Value = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return Value;
        }

        public static string Decrypt(string Value, string EncryptionKey)
        {
            if (EncryptionKey.Trim() == "")
                EncryptionKey = "AMRESH";
            byte[] buffer = Convert.FromBase64String(Value);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(EncryptionKey, new byte[13]
                {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
                });
                aes.Key = rfc2898DeriveBytes.GetBytes(32);
                aes.IV = rfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(buffer, 0, buffer.Length);
                        cryptoStream.Close();
                    }
                    Value = Encoding.Unicode.GetString(memoryStream.ToArray());
                }
            }
            return Value;
        }
        public static string SecureHashSHA256(string Password) => GetStringFromHash(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("X2"));
            return stringBuilder.ToString();
        }
    }
}