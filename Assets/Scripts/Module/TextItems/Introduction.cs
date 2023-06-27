namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;

    /// <summary>
    /// 介绍文本内容
    /// </summary>
    internal class Introduction : TextItem  
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Content";

        /// <summary>
        /// The text language key.
        /// </summary>
        internal override int textLanguageKey => LanguageKey.kIntroductionContent;
    }
}
