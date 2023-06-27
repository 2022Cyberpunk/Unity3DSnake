namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.TextItems;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.GameCaption;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 公告按钮
    /// </summary>
    public class AnnouncementItem : ButtonItem  
    {
        /// <summary>
        /// 公告内容
        /// </summary>
        private Announcement m_Announcement;

        /// <summary>
        /// 左侧背景图片
        /// </summary>
        private Image m_LeftImage;  

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
        /// 父节点
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform.transform.Find("Left"));
            m_Text.fontSize = 40;

            m_LeftImage = parenTransform.Find("Left").GetComponent<Image>();
            m_LeftImage.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftBackGround);

            m_Announcement = new Announcement();
            m_Announcement.Init(parenTransform.Find("Right/ScrollView/Viewport"));

            ShowItems();

            AddEvent();
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
        /// 注册事件
        /// </summary>
        public override void AddEvent()
        {
            base.AddEvent();
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, this.OnSystemThemeChange);
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            GameCaptionView.instance.HideAll();
            this.m_Announcement.Show();
        }

        /// <summary>
        /// 主题改变
        /// </summary>
        private void OnSystemThemeChange()
        {
            this.m_LeftImage.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftBackGround);
        }
    }
}
