namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;

    using UnityEngine;

    /// <summary>
    /// 敬请期待
    /// </summary>
    internal class ExpectItem : TextItem   
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Expect";

        /// <summary>
        /// The text language key.
        /// </summary>
        internal override int textLanguageKey => LanguageKey.kExpect;

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parentTransform">
        /// The parent transform.
        /// </param>
        public override void Init(Transform parentTransform)
        {
            base.Init(parentTransform);
            m_Text.fontSize = 150;
            this.Hide();
        }
    }
}
