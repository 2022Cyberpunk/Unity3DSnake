namespace Assets.Scripts.Module.ToggleItems
{
    using Assets.Libs.Config;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;

    using UnityEngine;

    /// <summary>
    /// 语言设置
    /// </summary>
    internal class LanguageSetting : ToggleItem
    {
        /// <summary>
        /// 父节点名称
        /// </summary>
        internal override string parentName => "LanguageSetting";

        /// <summary>
        /// 选项一节点名称
        /// </summary>
        internal override string optionOneName => "Chinese";

        /// <summary>
        /// 选项一显示文本
        /// </summary>
        internal override int optionOneLanguageKey => LanguageKey.kChinese;

        /// <summary>
        /// 选项二节点名称
        /// </summary>
        internal override string optionTwoName => "English";

        /// <summary>
        /// 选项二显示文本
        /// </summary>
        internal override int optionTwoLanguageKey => LanguageKey.kEnglish;

        /// <summary>
        /// Gets the option three name.
        /// </summary>
        internal override string optionThreeName { get; }

        /// <summary>
        /// Gets the option three language key.
        /// </summary>
        internal override string optionThreeLanguageKey { get; }

        /// <summary>
        /// 标题
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kLanguage;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parentTransform">
        /// The parent transform.
        /// </param>
        public override void Init(Transform parentTransform)
        {
            base.Init(parentTransform);
            m_TitleText.fontSize = 30;
            m_OptionOneText.fontSize = 30;
            m_OptionTwoText.fontSize = 30;
        }

        /// <summary>
        /// The init value.
        /// </summary>
        protected override void InitValue()
        {
            base.InitValue();
            m_OptionOne.isOn = GlobalSetting.instance.curLanguage == Language.Ch;
            this.m_OptionTwo.isOn = GlobalSetting.instance.curLanguage == Language.En;
        }

        /// <summary>
        /// The on option one value change.
        /// </summary>
        /// <param name="isOn">
        /// The is on.
        /// </param>
        protected override void OnOptionOneValueChange(bool isOn)
        {
            GlobalSetting.instance.curLanguage = isOn ? Language.Ch : Language.En;
            EventDispatcher.TriggerEvent(ConfigEvent.kSystemLanguageChange);
        }

        /// <summary>
        /// The on option two value change.
        /// </summary>
        /// <param name="isOn">
        /// The is on.
        /// </param>
        protected override void OnOptionTwoValueChange(bool isOn)
        {
            base.OnOptionTwoValueChange(isOn);
        }
    }
}
