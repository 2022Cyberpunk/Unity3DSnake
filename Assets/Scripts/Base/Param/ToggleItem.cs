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
    /// toggle选项基类
    /// </summary>
    public abstract class ToggleItem    
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
        /// toggle组
        /// </summary>
        protected ToggleGroup m_ToggleGroup;

        /// <summary>
        /// Toggle选项一
        /// </summary>
        protected Toggle m_OptionOne;

        /// <summary>
        /// Toggle选项一文本
        /// </summary>
        protected Text m_OptionOneText;

        /// <summary>
        /// Toggle选项一图片
        /// </summary>
        protected Image m_OptionOneImage;

        /// <summary>
        /// Toggle选项二
        /// </summary>
        protected Toggle m_OptionTwo;

        /// <summary>
        /// Toggle选项二文本
        /// </summary>
        protected Text m_OptionTwoText;

        /// <summary>
        /// Toggle选项二图片
        /// </summary>
        protected Image m_OptionTwoImage;

        /// <summary>
        /// Toggle选项三
        /// </summary>
        protected Toggle m_OptionThree;

        /// <summary>
        /// Toggle选项三文本
        /// </summary>
        protected Text m_OptionThreeText;

        /// <summary>
        /// Toggle选项三图片
        /// </summary>
        protected Image m_OptionThreeImage;

        /// <summary>
        /// Gets获取父节点name，由子类实现
        /// </summary>
        internal abstract string parentName { get; }

        /// <summary>
        /// Gets 选项一节点name，由子类实现
        /// </summary>
        internal abstract string optionOneName { get; }

        /// <summary>
        /// Gets 选项一字典语言key值，由子类实现
        /// </summary>  
        internal abstract int optionOneLanguageKey { get; }

        /// <summary>
        /// Gets 选项二节点name，由子类实现
        /// </summary>
        internal abstract string optionTwoName { get; }

        /// <summary>
        /// Gets 选项二字典语言key值，由子类实现
        /// </summary>  
        internal abstract int optionTwoLanguageKey { get; }

        /// <summary>
        /// Gets 选项三节点name，由子类实现
        /// </summary>
        internal abstract string optionThreeName { get; }

        /// <summary>
        ///  Gets 选项三字典语言key值，由子类实现
        /// </summary>  
        internal abstract string optionThreeLanguageKey { get; }

        /// <summary>
        /// 标题文本key值，由子类实现
        /// </summary>
        internal abstract int titleLanguageKey { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parentTransform">
        /// 父节点
        /// </param>
        public virtual void Init(Transform parentTransform)
        {
            m_Parent = parentTransform.Find(parentName);
            m_TitleText = m_Parent.Find("Title/Text").GetComponent<Text>();
            m_TitleText.text = LanguageModel.instance.GetCurLanguageValue(titleLanguageKey);
            m_TitleText.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            m_TitleText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);

            m_ToggleGroup = m_Parent.Find("Options").GetComponent<ToggleGroup>();
            m_OptionOne = m_ToggleGroup.transform.Find(optionOneName).GetComponent<Toggle>();
            m_OptionOne.group = m_ToggleGroup;
            m_OptionOne.onValueChanged.AddListener(OnOptionOneValueChange);
            m_OptionOneText = m_OptionOne.transform.Find("Text").GetComponent<Text>();
            m_OptionOneText.text = LanguageModel.instance.GetCurLanguageValue(optionOneLanguageKey);
            m_OptionOneText.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            m_OptionOneText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_OptionOneImage = m_OptionOne.transform.Find("BackGround").GetComponent<Image>();

            m_OptionTwo = m_ToggleGroup.transform.Find(optionTwoName).GetComponent<Toggle>();
            m_OptionTwo.group = m_ToggleGroup;
            m_OptionTwo.onValueChanged.AddListener(OnOptionTwoValueChange);
            m_OptionTwoText = m_OptionTwo.transform.Find("Text").GetComponent<Text>();
            m_OptionTwoText.text = LanguageModel.instance.GetCurLanguageValue(optionTwoLanguageKey);
            m_OptionTwoText.font = FontManager.instance.GeFont((int)GlobalSetting.instance.curFontStyle);
            m_OptionTwoText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_OptionTwoImage = m_OptionTwo.transform.Find("BackGround").GetComponent<Image>();

            AddEvent();
            InitValue();
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
        /// 选项一改变时调用
        /// </summary>
        /// <param name="isOn">
        /// The is on.
        /// </param>
        protected virtual void OnOptionOneValueChange(bool isOn)
        {
        }

        /// <summary>
        /// 选项二改变时调用
        /// </summary>
        /// <param name="isOn">
        /// The is on.
        /// </param>
        protected virtual void OnOptionTwoValueChange(bool isOn)
        {
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        protected virtual void InitValue()
        {
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        protected virtual void AddEvent()
        {
            EventDispatcher.AddEventListener(ConfigEvent.kSystemLanguageChange, OnSystemLanguageChange);
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, OnSystemThemeChange);
        }

        /// <summary>
        /// 主题改变调用
        /// </summary>
        protected void OnSystemThemeChange()
        {
            m_TitleText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_OptionOneText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
            m_OptionTwoText.color = ThemeModel.instance.GetColorValue(ColorKey.kText);
        }

        /// <summary>
        /// 语言改变时调用
        /// </summary>
        protected virtual void OnSystemLanguageChange()
        {
            m_TitleText.text = LanguageModel.instance.GetCurLanguageValue(titleLanguageKey);
            this.m_OptionOneText.text = LanguageModel.instance.GetCurLanguageValue(optionOneLanguageKey);
            this.m_OptionTwoText.text = LanguageModel.instance.GetCurLanguageValue(optionTwoLanguageKey);
        }

        /// <summary>
        /// 更新值
        /// </summary>
        protected virtual void UpdateValue()
        {
        }
    }
}
