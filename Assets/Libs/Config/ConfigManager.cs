namespace Assets.Libs.Config
{
    using System;
    using System.IO;
    using Assets.Scripts.Base;
    using Assets.Scripts.Module.Snake;
    using UnityEngine;
    using FileMode = System.IO.FileMode;

    /// <summary>
    /// 配置管理器，加载、保存配置
    /// </summary>
    public class ConfigManager : Singleton<ConfigManager>
    {
        /// <summary>
        /// 配置文件的文件头编码，禁止修改
        /// </summary>
        private const long kConfigFileHeader = 15175621787216;

        /// <summary>
        /// 保存当前配置
        /// </summary>
        public void SaveLastConfig()
        {
            var path = this.GetLastConfigPath();
            this.SaveConfig(path);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="path">保存路径</param>
        public void SaveConfig(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                if (dir != null)
                {
                    Directory.CreateDirectory(dir);
                }
            }

            //保存为cfg格式
            this.SaveConfigCfg(path);
        }

        /// <summary>
        /// 加载最新配置
        /// </summary>
        public void LoadLastConfig()
        {
            var path = this.GetLastConfigPath();

            if (!File.Exists(path))
            {
                //初始化全局设置
                GlobalSetting.instance.Init();
            }

            LoadConfig(path);
        }

        /// <summary>
        /// 加载默认配置
        /// </summary>
        public void LoadDefaultConfig()
        {
            TextAsset configBytes;
            var config = new GlobalSetting();
            configBytes = Resources.Load<TextAsset>("config/defaultConfig");

            this.LoadConfig(configBytes);
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="path">配置所在文件路径</param>
        public void LoadConfig(string path)
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    this.LoadConfig(fileStream);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"load config error,msg:{e.Message},stackTrace:{e.StackTrace}");
            }
        }

        /// <summary>
        /// 获取最新配置文件路径
        /// </summary>
        /// <returns>配置文件路径</returns>
        private string GetLastConfigPath()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer ||
                Application.platform == RuntimePlatform.WindowsEditor)
            {
                return $@"{Application.dataPath}\..\config\lastConfig.cfg";
            }

            return $@"{Application.persistentDataPath}/config/lastConfig.cfg";
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="configBytes">配置内容</param>
        /// <param name="initData">是否初始化数据 true:初始化 false:不初始化</param>
        private void LoadConfig(TextAsset configBytes, bool initData = true)
        {
            using (var stream = new MemoryStream(configBytes.bytes))
            {
                this.LoadConfig(stream, initData);
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="stream">配置文件流</param>
        /// <param name="initData">是否初始化数据 true:初始化 false:不初始化</param>
        private void LoadConfig(Stream stream, bool initData = true)
        {
            using (BinaryReader br = new BinaryReader(stream))
            {
                long header = br.ReadInt64();
                if (header == kConfigFileHeader)
                {
                    // 读取新版本的配置文件
                    var configFile = MsgHelper.DeserializeFormStream<Config>(stream, true);
                    configFile.ApplySetting();
                }
                else
                {
                   Debug.LogError("加载配置错误！");
                }
            }
        }

        /// <summary>
        /// 保存为cfg格式
        /// </summary>
        /// <param name="path">保存路径</param>
        private void SaveConfigCfg(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fileStream))
                {
                    bw.Write(kConfigFileHeader);         // 文件头，用于校验文件
                    var config = Snake.instance.config;
                    MsgHelper.SerializeToStream(fileStream, config, true);
                }
            }
        }
    }
}