using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public static string Send(MailMessage mm, string EmailFrom)
        {
            string MailError = "";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                string Port = System.Configuration.ConfigurationManager.AppSettings.Get("Port");

                string UserName = EmailFrom;
                string Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = Convert.ToInt32(Port);

                string Host = System.Configuration.ConfigurationManager.AppSettings.Get("MicrosoftHost");
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;

                smtp.Host = Host;
                smtp.Credentials = new NetworkCredential(UserName, Password);
                smtp.Timeout = 1000 * 60 * 5;
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                MailError = ex.Message;
                if (ex.InnerException != null)
                    MailError = "; Inner Exception: " + ex.InnerException;
            }
            return MailError;
        }
        public static string SendMailForContact(string Name, string EmailID, string Message)
        {
            string mBody;
            mBody = "<html><body><style>table,th,td,tr{ border-collapse:collapse; font-family:Arial, Helvetica, sans-serif; font-size:13px; padding:4px 13px; color:#000; border:1px solid #c5c5c5;}</style>";
            mBody += "<table border='1' width='100%'>";
            mBody += "<tr><td><br /><strong>Dear Sir/Mam</strong>,<br /><br />";
            mBody += " <strong> Receive below contact-us details  from EzRozgar website</strong><br/><br/>";
            mBody += "<table border='1'>";

            mBody += "<tr>";
            mBody += "<td valign='top'><b>Name </b></td>";
            mBody += "<td valign='top'>:</td>";
            mBody += "<td valign='top'>" + Name.Trim() + "</td>";
            mBody += "</tr>";

            mBody += "<tr>";
            mBody += "<td valign='top'><b>Email Address</b></td>";
            mBody += "<td valign='top'>:</td>";
            mBody += "<td valign='top'>" + EmailID.Trim() + "</td>";
            mBody += "</tr>";

            mBody += "<tr>";
            mBody += "<td valign='top'><b> Message </b></td>";
            mBody += "<td valign='top'>:</td>";
            mBody += "<td valign='top'>" + Message.Trim() + "</td>";
            mBody += "</tr>";
            mBody += "</table><br/>";

            mBody += "<br />";
            mBody += "<br /><br style='font-family:Arial,Helvetica, sans-serif;'>Thanks<br /><br />";
            mBody += "</body></html>";
            return mBody;
        }
    }
}