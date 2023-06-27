namespace Assets.Libs.Config
{
    using System;
    using System.IO;
    using MessagePack;
    using MessagePack.Resolvers;

    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public static class MsgHelper
    {
        /// <summary>
        /// 从bytes中反序列化对象
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="bytes">The bytes.</param>
        /// <param name="useCompression">是否LZ4压缩</param>
        /// <returns>T.</returns>
        public static T DeserializeFormBytes<T>(byte[] bytes, bool useCompression = false)
        {
            if (useCompression)
            {
                return MessagePackSerializer.Deserialize<T>(bytes, GetLz4BlockArray());
            }
            else
            {
                return MessagePackSerializer.Deserialize<T>(bytes);
            }
        }

        /// <summary>
        /// 从文件路径中，反序列化对象
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="path">The path.</param>
        /// <returns>T.</returns>
        public static T DeserializeFormPath<T>(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                return DeserializeFormStream<T>(fileStream);
            }
        }

        /// <summary>
        /// 从Stream中反序列化对旬
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="useCompression">if set to <c>true</c> [use compression].</param>
        /// <returns>T.</returns>
        public static T DeserializeFormStream<T>(Stream stream, bool useCompression = false)
        {
            if (useCompression)
            {
                return MessagePackSerializer.Deserialize<T>(stream, GetLz4BlockArray());
            }
            else
            {
                return MessagePackSerializer.Deserialize<T>(stream);
            }
        }

        /// <summary>
        /// 读取序列化文件
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="filePath">The file path.</param>
        /// <returns>T.</returns>
        public static T ReadFile<T>(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                return DeserializeFormStream<T>(fileStream);
            }
        }

        /// <summary>
        /// 序列化克隆
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="target">The target.</param>
        /// <returns>T.</returns>
        public static T SerializeClone<T>(T target)
        {
            var configBytes = SerializeToBytes(target);
            return DeserializeFormBytes<T>(configBytes);
        }

        /// <summary>
        /// 将对象序列化成bytes
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="useCompression">是否LZ4压缩</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] SerializeToBytes<T>(T target, bool useCompression = false)
        {
            if (useCompression)
            {
                return MessagePackSerializer.Serialize(target, GetLz4BlockArray());
            }
            else
            {
                return MessagePackSerializer.Serialize(target);
            }
        }

        /// <summary>
        /// 将对象序列化到Stream里
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="target">The target.</param>
        /// <param name="useCompression">是否LZ4压缩</param>
        public static void SerializeToStream<T>(Stream stream, T target, bool useCompression = false)
        {
            if (useCompression)
            {
                MessagePackSerializer.Serialize(stream, target, GetLz4BlockArray());
            }
            else
            {
                MessagePackSerializer.Serialize(stream, target);
            }
        }

        /// <summary>
        /// 写入序列化文件
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WriteFile<T>(T target, string filePath, FileMode fileMode)
        {
            FileStream fileStream = null;
            try
            {
                var bytes = SerializeToBytes(target);
                fileStream = new FileStream(filePath, fileMode);
                fileStream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError(ex.Message);
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            return true;
        }

        /// <summary>
        /// 获取数据压缩配置
        /// </summary>
        /// <returns>MessagePackSerializerOptions.</returns>
        private static MessagePackSerializerOptions GetLz4BlockArray()
        {
            // 添加解析器
            var resolver = CompositeResolver.Create(
                StandardResolver.Instance,
                MessagePack.Unity.UnityResolver.Instance,
                BuiltinResolver.Instance,
                AttributeFormatterResolver.Instance,
                GeneratedResolver.Instance,
               ContractlessStandardResolver.Instance);

            var options = StandardResolverAllowPrivate.Options.WithCompression(MessagePackCompression.Lz4BlockArray)
                .WithResolver(resolver);
            return options;
        }
    }
}