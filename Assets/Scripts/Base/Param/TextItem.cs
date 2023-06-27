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
    /// 单文本显示基类
    /// </summary>
    public abstract class TextItem
    {   
        /// <summary>
        /// 父节点
        /// </summary>
        protected Transform m_Parent;

        /// <summary>
        /// 显示文本
        /// </summary>
        protected Text m_Text;

        /// <summary>
        /// 宽度
        /// </summary>
        protected float m_Width;

        /// <summary>
        /// 高度
        /// </summary>
        protected float m_Height;

        /// <summary>
        /// 获取文本节点name，由子类实现
        /// </summary>
        internal abstract string name { get; }

        /// <summary>
        ///获取文本字典key值，由子类实现
        /// </summary>
        internal abstract int textLanguageKey { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parentTransform">
        /// 父节点
        /// </param>
        public virtual void Init(Transform parentTransform)
        {
            m_Parent = parentTransform.Find(name);
            m_Text = this.m_Parent.Find("Text").GetComponent<Text>();
            m_Text.text = LanguageModel.instance.GetCurLanguageValue(textLanguageKey);
            m_Text.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            this.m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_Text.fontSize = 30;
            this.AddEvent();
            this.InitValue();
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        public virtual void InitValue()
        {
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            this.m_Parent.transform.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            this.m_Parent.transform.gameObject.SetActive(false);
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
        protected virtual void UpdateValue()
        {
        }

        /// <summary>
        /// 语言改变调用
        /// </summary>
        private void OnSystemLanguageChange()
        {
            this.m_Text.text = LanguageModel.instance.GetCurLanguageValue(this.textLanguageKey);
        }

        /// <summary>
        /// 主题改变调用
        /// </summary>
        private void OnSystemThemeChange()
        {
            this.m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
        }
    }
}
