using System.Collections.Generic;

namespace Assets.Scripts.SpriteManager
{
    using System;

    using Assets.Libs.Config;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base;
    using Assets.Scripts.LogManager;

    using UnityEngine;

    using FontStyle = Assets.Libs.Enum.FontStyle;

    /// <summary>
    /// 图集加载统一管理
    /// </summary>
    public class FontManager : Singleton<FontManager> 
    {
        /// <summary>
        /// 字体库
        /// </summary>
        private readonly List<Font> m_Fonts = new List<Font>(10);


        /// <summary>
        /// 初始化，加载
        /// </summary>
        public void Init()
        {
            //字体
            m_Fonts.Add(Resources.Load<Font>("Font/Yueyuan"));
            try
            {
                foreach (var font in this.m_Fonts)
                {
                    if (font is null)
                    {
                        LogManager.instance.LogToFile($"字体加载错误{font.name}", LogLevel.Information);
                    }
                    else
                    {
                        LogManager.instance.LogToFile($"{m_Fonts.Count}种字体加载成功", LogLevel.Information);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// 获取食物图集
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Sprite"/>.
        /// </returns>
        public Font GeFont(int index)  
        {
            var curFoodSprite = GlobalSetting.instance.curFontStyle;
            switch (curFoodSprite)
            {   
                case FontStyle.YueYuan:
                    return m_Fonts[index];
                default:
                    Debug.Log($"未知的字体{curFoodSprite}");
                    break;
            }

            return null;
        }
    }
}
