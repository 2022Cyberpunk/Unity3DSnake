namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;

    using UnityEngine;

    /// <summary>
    /// 得分加成
    /// </summary>
    public class ScoreBonusItem : DoubleTextItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "ScoreBonus";

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
        internal override int titleLanguageKey => LanguageKey.kScoreBonus;

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parentTransform">
        /// The parent transform.
        /// </param>
        public override void Init(Transform parentTransform)
        {
            base.Init(parentTransform);
            value = Snake.instance.bonus;
        }

        /// <summary>
        /// The init value.
        /// </summary>
        public override void InitValue()
        {
            base.InitValue();
            m_ValueText.text = value.ToString(this.m_Format);
        }

        /// <summary>
        /// The update value.
        /// </summary>
        public override void UpdateValue()
        {
            base.UpdateValue();
            value = Snake.instance.bonus;
            m_ValueText.text = value.ToString(this.m_Format);
        }
    }
}
