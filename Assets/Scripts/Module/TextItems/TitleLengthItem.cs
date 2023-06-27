namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;

    using UnityEngine;

    /// <summary>
    /// 长度
    /// </summary>
    internal class TitleLengthItem : TextItem     
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Length";

        /// <summary>
        /// The text language key.
        /// </summary>
        internal override int textLanguageKey => LanguageKey.kLength;

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
