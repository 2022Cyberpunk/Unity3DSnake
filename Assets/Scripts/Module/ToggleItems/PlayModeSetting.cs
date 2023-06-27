namespace Assets.Scripts.Module.ToggleItems
{
    using Assets.Libs.Config;
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;

    using UnityEngine;

    using PlayMode = Assets.Libs.Enum.PlayMode;

    /// <summary>
    /// 模式设置
    /// </summary>
    internal class PlayModeSetting : ToggleItem     
    {
        /// <summary>
        /// 父节点名称
        /// </summary>
        internal override string parentName => "PlayModeSetting";   

        /// <summary>
        /// 选项一节点名称
        /// </summary>
        internal override string optionOneName => "Classic";

        /// <summary>
        /// 选项一显示文本
        /// </summary>
        internal override int optionOneLanguageKey => LanguageKey.kClassic;

        /// <summary>
        /// 选项二节点名称
        /// </summary>
        internal override string optionTwoName => "Freedom";

        /// <summary>
        /// 选项二显示文本
        /// </summary>
        internal override int optionTwoLanguageKey => LanguageKey.kFreedom;

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
        internal override int titleLanguageKey => LanguageKey.kPlayMode;

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
            m_OptionOne.isOn = GlobalSetting.instance.curPlayMode == PlayMode.Classic;
        }

        /// <summary>
        /// The on option one value change.
        /// </summary>
        /// <param name="isOn">
        /// The is on.
        /// </param>
        protected override void OnOptionOneValueChange(bool isOn)
        {
            GlobalSetting.instance.curPlayMode = isOn ? PlayMode.Classic : PlayMode.Freedom;
            EventDispatcher.TriggerEvent(ConfigEvent.kPlayModeChange);
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
