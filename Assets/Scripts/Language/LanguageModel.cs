using System.Collections.Generic;

namespace Assets.Scripts.Language
{
    using System.IO;
    using System.Xml;
    using Assets.Libs.Config;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base;
    using Assets.Scripts.LogManager;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>
    /// 语言模型
    /// </summary>
    public class LanguageModel : Singleton<LanguageModel>
    {
        /// <summary>
        /// 中文字典
        /// </summary>
        [NotNull]
        private readonly Dictionary<int, string> m_ChDictionary = new Dictionary<int, string>();

        /// <summary>
        /// 英文字典
        /// </summary>
        private readonly Dictionary<int, string> m_EhDictionary = new Dictionary<int, string>();

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="value">    
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCurLanguageValue(int value)    
        {
            if (GlobalSetting.instance.curLanguage == Language.Ch)
            {
                return this.m_ChDictionary[value];
            }
            else if (GlobalSetting.instance.curLanguage == Language.En)
            {
                return this.m_EhDictionary[value];
            }
            else
            {
                Debug.LogError($"不支持的语言类型{GlobalSetting.instance.curLanguage}");
                return null;
            }
        }

        /// <summary>
        /// 初始化xml字典
        /// </summary>
        public void Init()
        {
            var path = Path.Combine(Application.streamingAssetsPath, "Language.xml");
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path);
                XmlNodeList zhDictionaryNodes = xmlDoc.SelectNodes("/Snake/ZhDictinary/item");
                if (zhDictionaryNodes != null)
                {
                    foreach (XmlNode node in zhDictionaryNodes)
                    {
                        if (node.Attributes != null)
                        {
                            string idStr = node.Attributes["id"].Value;
                            string content = node.InnerText;
                            if (int.TryParse(idStr, out var id))
                            {
                                this.m_ChDictionary.Add(id, content);
                            }
                        }
                    }
                }

                XmlNodeList enDictionaryNodes = xmlDoc.SelectNodes("/Snake/EnDictinary/item");
                if (enDictionaryNodes != null)
                {
                    foreach (XmlNode node in enDictionaryNodes)
                    {
                        if (node.Attributes != null)
                        {
                            string idStr = node.Attributes["id"].Value;
                            string content = node.InnerText;
                            if (int.TryParse(idStr, out var id))
                            {
                                this.m_EhDictionary.Add(id, content);
                            }
                        }
                    }
                }

                LogManager.instance.LogToFile("语言文档加载成功", LogLevel.Information);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load language XML file: " + e.Message);
            }
        }
    }
}
