namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.TextItems;
    using Assets.Scripts.Module.ToggleItems;

    using UnityEngine;

    /// <summary>
    /// 游戏玩法
    /// </summary>
    public class GameplayItem : ButtonItem  
    {
        /// <summary>
        /// 公告内容
        /// </summary>
        private Announcement m_Announcement;

        /// <summary>
        /// 按钮节点名称
        /// </summary>
        internal override string name => "AnnouncementBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kAnnouncement;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform.transform.Find("Left"));
            
            m_Announcement = new Announcement();
            this.m_Announcement.Init(parenTransform.Find("Right/ScrollView/Viewport"));
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void ShowItems()     
        {
            this.m_Announcement.Show();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void HideItems() 
        {
            this.m_Announcement.Hide();
        }

        /// <summary>
        /// The btn click.
        /// </summary>
        internal override void BtnClick()
        {
            this.m_Announcement.Show();
        }
    }
}
