using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using static System.String;
using System.Security.Cryptography;
using System.Text;

namespace PhotoGalerie
{
    public class BinaryCache
    {
        private BinaryCache() { }

        private static BinaryCache _instance = null;
        private static object _lock = new object();

        public static BinaryCache Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    if (_instance != null) return _instance;
                    _instance = new BinaryCache();
                }

                return _instance;
            }
        }

        private string Folder
        {
            get
            {
                return Config.BinaryCacheFolder;
            }
        }

        private string GetFilaPath(string key)
        {
            return Path.Combine(Folder, GetMD5(key) + ".cache");
        }
        public void Store(string key, byte[] data)
        {
            string filePath = GetFilaPath(key);
            File.WriteAllBytes(filePath, data);
        }

        public byte[] Load(string key)
        {
            string filePath = GetFilaPath(key);
            if (!File.Exists(filePath)) return null;

            return File.ReadAllBytes(filePath);
        }

        private static string GetMD5(string input)
        {

            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();

        }
    }
}