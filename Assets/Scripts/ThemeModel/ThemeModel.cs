using System.Collections.Generic;

namespace Assets.Scripts.ThemeModel
{
    using Assets.Libs.Config;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base;

    using UnityEngine;

    /// <summary>
    /// 主题模型
    /// </summary>
    public class ThemeModel : Singleton<ThemeModel>
    {
        /// <summary>
        /// 浅色主题颜色字典
        /// </summary>
        private readonly Dictionary<int, Color> m_LightDictionary = new Dictionary<int, Color>();

        /// <summary>
        /// 深色主题颜色字典
        /// </summary>  
        private readonly Dictionary<int, Color> m_DarkDictionary = new Dictionary<int, Color>();

        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="value">    
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public Color GetColorValue(int value)   
        {
            if (GlobalSetting.instance.curTheme == Theme.Light)
            {
                return this.m_LightDictionary[value];
            }
            else if (GlobalSetting.instance.curTheme == Theme.Dark)
            {
                return this.m_DarkDictionary[value];
            }
            else
            {
                Debug.Log($"不支持的主题类型{GlobalSetting.instance.curLanguage}");
                return this.m_LightDictionary[value]; 
            }
        }

        /// <summary>
        /// 初始化颜色表
        /// </summary>
        public void Init()
        {
            m_LightDictionary.Add(0, new Color(180 / 255f, 180 / 255f, 180 / 255f, 1f));
            m_LightDictionary.Add(1, new Color(160 / 255f, 160 / 255f, 160 / 255f, 0.2f));
            m_LightDictionary.Add(2, new Color(180 / 255f, 180 / 255f, 180 / 255f, 1f));
            m_LightDictionary.Add(3, Color.white);
            m_LightDictionary.Add(4, new Color(0f, 101 / 255f, 255 / 255f, 0.8f));
            m_LightDictionary.Add(5, new Color(0f, 122 / 255f, 204 / 255f, 0.8f));
            m_LightDictionary.Add(6, Color.black);
            m_LightDictionary.Add(7, new Color(0.8f, 0.8f, 0.8f, 0.1f));

            m_DarkDictionary.Add(0, new Color(40 / 255f, 40 / 255f, 40 / 255f, 1f));
            m_DarkDictionary.Add(1, new Color(80 / 255f, 80 / 255f, 80 / 255f, 0.2f));
            m_DarkDictionary.Add(2, Color.white);
            m_DarkDictionary.Add(3, Color.grey);
            m_DarkDictionary.Add(4, new Color(0 / 255f, 83 / 255f, 216 / 255f, 0.9f));
            m_DarkDictionary.Add(5, new Color(7 / 255f, 71 / 255f, 166 / 255f, 0.8f));
            m_DarkDictionary.Add(6, Color.black);
            m_DarkDictionary.Add(7, new Color(0.8f, 0.8f, 0.8f, 0.1f));
        }
    }
}
