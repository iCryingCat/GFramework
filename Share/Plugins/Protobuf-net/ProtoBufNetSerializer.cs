using System;
using System.IO;

using ProtoBuf;

namespace GFramework.Network
{
    // ProtocolBuf Net序列化
    public class ProtoBufNetSerializer<T>
    {
        private ProtoBufNetSerializer() { }

        /// <summary>
        /// 序列化对象得到字节
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Encode(T obj)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize<T>(ms, obj);
                    byte[] buffer = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 反序列化字节得到对象
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static T Decode(byte[] msg)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(msg, 0, msg.Length);
                    ms.Position = 0;
                    T res = Serializer.Deserialize<T>(ms);
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
