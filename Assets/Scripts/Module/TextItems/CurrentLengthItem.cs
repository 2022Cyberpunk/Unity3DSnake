namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;

    /// <summary>
    /// 当前长度
    /// </summary>
    public class CurrentLengthItem : DoubleTextItem   
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "CurLength";

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
        internal override int titleLanguageKey => LanguageKey.kCurrentLength;

        /// <summary>
        /// The init value.
        /// </summary>
        public override void InitValue()
        {
            base.InitValue();
            value = Snake.instance.length;
            this.m_ValueText.text = value.ToString(this.m_Format);
        }

        /// <summary>
        /// The update value.
        /// </summary>
        public override void UpdateValue()
        {
            base.UpdateValue();
            value = Snake.instance.length;
            this.m_ValueText.text = value.ToString(this.m_Format);
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
