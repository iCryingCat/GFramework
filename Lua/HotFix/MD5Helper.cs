using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GFramework
{
    /// <summary>
    /// 生成MD5码
    /// </summary>
    public class MD5Helper
    {
        public static string Encode(string file)
        {
            try
            {
                using (FileStream stream = new FileStream(file, FileMode.Open))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] buffer = md5.ComputeHash(stream);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        // X2转化为16进制
                        sb.Append(buffer[i].ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
    }
}
