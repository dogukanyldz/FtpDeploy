using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;

namespace Grid.Data
{
    public class Database
    {
        private string _ServerName;
        public string ServerName
        {
            get { return _ServerName; }
            set { _ServerName = value; }
        }
        private string _DbName;
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }
        private string _DbUserName;
        public string DbUserName
        {
            get { return _DbUserName; }
            set { _DbUserName = value; }
        }
        private string _DbPassword;
        public string DbPassword
        {
            get { return _DbPassword; }
            set { _DbPassword = value; }
        }
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _DbCid;
        public string DbCid
        {
            get { return _DbCid; }
            set { _DbCid = value; }
        }

        private string _UsersDatabases;
        public string UsersDatabases
        {
            get { return _UsersDatabases; }
            set { _UsersDatabases = value; }
        }

        private string _ContextInfo;
        public string ContextInfo
        {
            get { return _ContextInfo; }
            set { _ContextInfo = value; }
        }

        public Database()
        {
            UsersDatabases = "UsersDatabases";
        }
        public static SqlConnection SqlConnection(string ServerName, string DbName, string DbUserName, string DbPassword)
        {
            //Integrated Security=SSPI
            if ((String.IsNullOrEmpty(DbUserName) || String.IsNullOrEmpty(DbPassword)) || (DbUserName.ToLower() == "null" || DbPassword.ToLower() == "null"))
            {              
                return new SqlConnection(@"Data Source = " + ServerName + "; Initial Catalog = " + DbName + "; Integrated Security = SSPI; Connection Timeout = 30");
            }

            else
            {
              //  DbPassword = DecryptCombined(DbPassword, "!TurnKey!");
                return new SqlConnection(@"Data Source = " + ServerName + "; Initial Catalog = " + DbName + "; Connection Timeout = 30; Connection Lifetime = 1000; Persist Security Info = True; User ID = " + DbUserName + "; Password = " + DbPassword);
            }
        }

        public static string DecryptCombined(string FromSql, string Password)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(Password);
            FromSql = FromSql.Substring(2);
            int version = BitConverter.ToInt32(StringToByteArray(FromSql.Substring(0, 8)), 0);
            byte[] encrypted = null;
            HashAlgorithm hashAlgo = null;
            SymmetricAlgorithm cryptoAlgo = null;
            int keySize = (version == 1 ? 16 : 32);
            if (version == 1)
            {
                hashAlgo = SHA1.Create();
                cryptoAlgo = TripleDES.Create();
                cryptoAlgo.IV = StringToByteArray(FromSql.Substring(8, 16));
                encrypted = StringToByteArray(FromSql.Substring(24));
            }
            else if (version == 2)
            {
                hashAlgo = SHA256.Create();
                cryptoAlgo = Aes.Create();
                cryptoAlgo.IV = StringToByteArray(FromSql.Substring(8, 32));
                encrypted = StringToByteArray(FromSql.Substring(40));
            }
            else
            {
                throw new Exception("Desteklenmeyen þifreleme");
            }
            cryptoAlgo.Padding = PaddingMode.PKCS7;
            cryptoAlgo.Mode = CipherMode.CBC;
            hashAlgo.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);
            cryptoAlgo.Key = hashAlgo.Hash.Take(keySize).ToArray();
            byte[] decrypted = cryptoAlgo.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
            int decryptLength = BitConverter.ToInt16(decrypted, 6);
            UInt32 magic = BitConverter.ToUInt32(decrypted, 0);
            if (magic != 0xbaadf00d)
            {
                throw new Exception("Þifre çözme baþarýsýz oldu");
            }

            byte[] decryptedData = decrypted.Skip(8).ToArray();
            bool isUtf16 = (Array.IndexOf(decryptedData, (byte)0) != -1);
            string decryptText = (isUtf16 ? Encoding.Unicode.GetString(decryptedData) : Encoding.UTF8.GetString(decryptedData));

            return decryptText;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
