namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;

    using UnityEngine;

    /// <summary>
    /// 时间
    /// </summary>
    internal class TitleTimeItem : TextItem     
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Time";

        /// <summary>
        /// The text language key.
        /// </summary>
        internal override int textLanguageKey => LanguageKey.kTime;

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
