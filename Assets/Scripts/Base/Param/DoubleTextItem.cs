namespace Assets.Scripts.Base.Param
{
    using Assets.Libs.Config;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.SpriteManager;
    using Assets.Scripts.ThemeModel;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 双文本显示基类
    /// </summary>
    public abstract class DoubleTextItem
    {
        /// <summary>
        /// 父节点
        /// </summary>
        protected Transform m_Parent;

        /// <summary>
        /// 标题文本
        /// </summary>
        protected Text m_TitleText;

        /// <summary>
        /// 值文本
        /// </summary>
        protected Text m_ValueText;

        /// <summary>
        /// 字符串格式
        /// </summary>
        protected string m_Format = "F1";

        /// <summary>
        /// 参数文本
        /// </summary>
        protected string m_ParamText;

        /// <summary>
        /// 宽度
        /// </summary>
        protected float m_Width;

        /// <summary>
        /// 高度
        /// </summary>
        protected float m_Height;

        /// <summary>
        /// Gets the name.
        /// </summary>
        internal abstract string name { get; }

        /// <summary>
        /// Gets值字符串，由子类实现
        /// </summary>
        internal abstract string strValue { get; set; }

        /// <summary>
        /// 获取值，由子类实现
        /// </summary>
        internal abstract float value { get; set; }

        /// <summary>
        /// 获取语言字典key值，由子类实现
        /// </summary>
        internal abstract int titleLanguageKey { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parentTransform">
        /// The parent transform.
        /// </param>
        public virtual void Init(Transform parentTransform)
        {
            m_Parent = parentTransform.Find(name);
            m_TitleText = m_Parent.Find("Title").GetComponent<Text>();
            m_ValueText = m_Parent.Find("Param").GetComponent<Text>();
            m_TitleText.text = LanguageModel.instance.GetCurLanguageValue(titleLanguageKey);
            m_TitleText.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            m_ValueText.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            m_TitleText.fontSize = 35;
            m_ValueText.fontSize = 35;
            m_TitleText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_ValueText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            AddEvent();
            InitValue();
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        public virtual void InitValue()
        {
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        public virtual void AddEvent()
        {
            EventDispatcher.AddEventListener(ConfigEvent.kSystemLanguageChange, OnSystemLanguageChange);
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, OnSystemThemeChange);
        }

        /// <summary>
        /// 更新值
        /// </summary>
        public virtual void UpdateValue()
        {
        }

        /// <summary>
        /// 显示
        /// </summary>
        protected void Show()
        {
           m_Parent.transform.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        protected void Hide()
        {
            m_Parent.transform.gameObject.SetActive(false);
        }

        /// <summary>
        /// 语言改变时调用
        /// </summary>
        private void OnSystemLanguageChange()
        {
            m_TitleText.text = LanguageModel.instance.GetCurLanguageValue(this.titleLanguageKey);
        }

        /// <summary>
        /// 主题改变时调用
        /// </summary>
        private void OnSystemThemeChange()
        {
            m_TitleText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_ValueText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
        }
    }
}
