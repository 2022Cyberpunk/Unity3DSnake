namespace Assets.Scripts.Module.TextItems
{
    using Assets.Libs.Config;
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;

    using UnityEngine;
    using UnityEngine.UI;

    using PlayMode = Assets.Libs.Enum.PlayMode;

    /// <summary>
    /// 当前模式
    /// </summary>
    public class CurrentModeItem : DoubleTextItem
    {
        /// <summary>
        /// The m_ title.
        /// </summary>
        private Text m_Title;

        /// <summary>
        /// The m_ mode.
        /// </summary>
        private Text m_Mode;

        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "CurMode";

        /// <summary>
        /// Gets or sets the str value.
        /// </summary>
        internal override string strValue { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        internal override float value { get; set; }

        /// <summary>
        /// The title language key.
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kCurrentMode;

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parentTransform">
        /// The parent transform.
        /// </param>
        public override void Init(Transform parentTransform)
        {
            base.Init(parentTransform);
            this.UpdateValue();
            this.m_ValueText.text = strValue;
        }

        /// <summary>
        /// The add event.
        /// </summary>
        public override void AddEvent()
        {
            base.AddEvent();
            EventDispatcher.AddEventListener(ConfigEvent.kPlayModeChange, this.UpdateValue);
        }

        /// <summary>
        /// The update value.
        /// </summary>
        public override void UpdateValue()
        {
            base.UpdateValue();
            if (GlobalSetting.instance.curPlayMode == PlayMode.Classic)
            {
                strValue = LanguageModel.instance.GetCurLanguageValue(LanguageKey.kClassic);
            }
            else if (GlobalSetting.instance.curPlayMode == PlayMode.Classic)
            {
                strValue = LanguageModel.instance.GetCurLanguageValue(LanguageKey.kFreedom);
            }

            m_ValueText.text = strValue;
        }
    }
}
