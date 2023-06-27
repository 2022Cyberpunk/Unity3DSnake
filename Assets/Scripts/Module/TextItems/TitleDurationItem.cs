namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using UnityEngine;

    /// <summary>
    /// 时长
    /// </summary>
    internal class TitleDurationItem : TextItem     
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Duration";

        /// <summary>
        /// The text language key.
        /// </summary>
        internal override int textLanguageKey => LanguageKey.kDuration;

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parentTransform">
        /// The parent transform.
        /// </param>
        public override void Init(Transform parentTransform)
        {
            base.Init(parentTransform);
            m_Text.fontSize = 30;
            this.Hide();
        }
    }
}
