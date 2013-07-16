using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace eulei.shop.Code
{
    /// <summary>
    /// 类的复制工具类（通过反序列化实现）。
    /// </summary>
    public class CloneObjectEx
    {
        public static object Copy(object ObjectInstance)
        {

            BinaryFormatter bFormatter = new BinaryFormatter();

            MemoryStream stream = new MemoryStream();

            bFormatter.Serialize(stream, ObjectInstance);

            stream.Seek(0, SeekOrigin.Begin);

            return bFormatter.Deserialize(stream);

        }



    }
}