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
    /// 按钮基类
    /// </summary>
    public abstract class ButtonItem
    {
        /// <summary>
        /// 按钮
        /// </summary>
        protected Button m_Button;

        /// <summary>
        /// 按钮文本
        /// </summary>
        protected Text m_Text;

        /// <summary>
        /// 按钮图片
        /// </summary>
        protected Image m_Image;

        /// <summary>
        /// 父节点
        /// </summary>  
        protected Transform m_Parent;

        /// <summary>
        /// 按钮
        /// </summary>
        internal Button button => m_Button;

        /// <summary>
        /// Gets按钮名称，子类实现
        /// </summary>
        internal abstract string name { get; }

        /// <summary>
        /// Gets字典编号，子类实现
        /// </summary>
        internal abstract int titleLanguageKey { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public virtual void Init(Transform parenTransform)
        {
            m_Parent = parenTransform;
            m_Button = m_Parent.Find(name).GetComponent<Button>();
            m_Text = m_Button.transform.Find("Text").GetComponent<Text>();
            m_Text.text = LanguageModel.instance.GetCurLanguageValue(titleLanguageKey);
            m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_Text.fontSize = 35;
            m_Text.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            m_Image = m_Button.transform.Find("Image").GetComponent<Image>();
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftButton);
            m_Button.onClick.AddListener(BtnClick);
            AddEvent();
            InitValue();
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
        /// 按钮点击,抽象方法由子类实现
        /// </summary>
        internal abstract void BtnClick();

        /// <summary>
        /// 显示
        /// </summary>
        internal virtual void Show()
        {
            this.m_Parent.gameObject.SetActive(true);
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        protected virtual void InitValue()
        {
        }

        /// <summary>
        /// 取消注册事件
        /// </summary>
        protected virtual void RemoveEvent()
        {
            EventDispatcher.RemoveEventListener(ConfigEvent.kSystemLanguageChange, OnSystemLanguageChange);
            EventDispatcher.RemoveEventListener(ConfigEvent.kSystemThemeChange, OnSystemThemeChange);
        }

        /// <summary>
        /// 语言改变时调用
        /// </summary>
        private void OnSystemLanguageChange()
        {
            this.m_Text.text = LanguageModel.instance.GetCurLanguageValue(this.titleLanguageKey);
        }

        /// <summary>
        /// 主题改变时调用
        /// </summary>
        private void OnSystemThemeChange()
        {
            this.m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftButton);
            this.m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
        }
    }
}
