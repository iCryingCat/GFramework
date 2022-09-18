// using System;
// using System.IO;
// using GFramework;
// using Protobuf;
//
// namespace CatFM.Net
// {
//    /// <summary>
//    /// ProtocolBuf Net序列化
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class ProtoBufNetSerializer<T>
//    {
//        private ProtoBufNetSerializer() { }
//
//        /// <summary>
//        /// 序列化对象得到字节
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public static byte[] Serialize(T obj)
//        {
//            try
//            {
//                using (MemoryStream ms = new MemoryStream())
//                {
//                    Serializer.Serialize<T>(ms, obj);
//                    byte[] buffer = new byte[ms.Length];
//                    ms.Position = 0;
//                    ms.Read(buffer, 0, buffer.Length);
//                    return buffer;
//                }
//            }
//            catch (Exception e)
//            {
//                GLogger.P(e.ToString());
//                return null;
//            }
//        }
//
//        /// <summary>
//        /// 反序列化字节得到对象
//        /// </summary>
//        /// <param name="msg"></param>
//        /// <returns></returns>
//        public static T Deserialize(byte[] msg)
//        {
//            try
//            {
//                using (MemoryStream ms = new MemoryStream())
//                {
//                    ms.Write(msg, 0, msg.Length);
//                    ms.Position = 0;
//                    T res = Serializer.Deserialize<T>(ms);
//                    return res;
//                }
//            }
//            catch (Exception e)
//            {
//                Bug.Throw(e.ToString());
//                return default(T);
//            }
//        }
//    }
// }
