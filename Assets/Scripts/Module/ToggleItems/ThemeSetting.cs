namespace Assets.Scripts.Module.ToggleItems
{
    using Assets.Libs.Config;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;

    using UnityEngine;

    /// <summary>
    /// 主题设置
    /// </summary>
    internal class ThemeSetting : ToggleItem    
    {
        /// <summary>
        /// 父节点名称
        /// </summary>
        internal override string parentName => "ThemeSetting";

        /// <summary>
        /// 选项一节点名称
        /// </summary>
        internal override string optionOneName => "Light";

        /// <summary>
        /// 选项一显示文本
        /// </summary>
        internal override int optionOneLanguageKey => LanguageKey.kLight;

        /// <summary>
        /// 选项二节点名称
        /// </summary>
        internal override string optionTwoName => "Dark";

        /// <summary>
        /// 选项二显示文本
        /// </summary>
        internal override int optionTwoLanguageKey => LanguageKey.kDark;

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
        internal override int titleLanguageKey => LanguageKey.kTheme;

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
            m_OptionOne.isOn = GlobalSetting.instance.curTheme == Theme.Light;
            this.m_OptionTwo.isOn = GlobalSetting.instance.curTheme == Theme.Dark;
        }

        /// <summary>
        /// The on option one value change.
        /// </summary>
        /// <param name="isOn">
        /// The is on.
        /// </param>
        protected override void OnOptionOneValueChange(bool isOn)
        {
            GlobalSetting.instance.curTheme = isOn ? Theme.Light : Theme.Dark;
            EventDispatcher.TriggerEvent(ConfigEvent.kSystemThemeChange);
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
