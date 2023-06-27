namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.GameParam;
    using Assets.Scripts.Module.Snake;

    /// <summary>
    /// 历史最高分
    /// </summary>
    public class CurrentSpeedItem : DoubleTextItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "CurSpeed";

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
        internal override int titleLanguageKey => LanguageKey.kCurrentSpeed;

        /// <summary>
        /// The update value.
        /// </summary>
        public override void UpdateValue()
        {
            base.UpdateValue();
            value = Snake.instance.Speed;
            this.m_ValueText.text = value.ToString();
        }

        /// <summary>
        /// The add event.
        /// </summary>
        public override void AddEvent()
        {
            base.AddEvent();
            EventDispatcher.AddEventListener(ConfigEvent.kRestartChange, this.UpdateValue);
        }
    }
}
