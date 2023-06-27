namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;

    /// <summary>
    /// 公告文本内容
    /// </summary>
    internal class Announcement : TextItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "Content";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int textLanguageKey => LanguageKey.kAnnouncementContent;
    }
}
