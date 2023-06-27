namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;

    /// <summary>
    /// 单局时长
    /// </summary>
    public class DurationItem : DoubleTextItem   
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Duration";

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
        internal override int titleLanguageKey => LanguageKey.kDuration;

        /// <summary>
        /// The update value.
        /// </summary>
        public override void UpdateValue()
        {
            base.UpdateValue();
            strValue = Snake.instance.TimeStr;
            this.m_ValueText.text = strValue;
        }

        /// <summary>
        /// The add event.
        /// </summary>
        public override void AddEvent()
        {
            base.AddEvent();
            EventDispatcher.AddEventListener(ConfigEvent.kLengthChange, this.UpdateValue);
        }
    }
}
